using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using dNetwork;
using Ionic.Zip;
using Lightness;
using Lightness.DNetwork;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;

namespace LEScripts
{
    public static class OnlineUpdate
    {
        public enum State
        {
            None,
            Auth,
            Download,
            Verify,
            Update,
            Restart
        }

        public static State NowState;

        public static ZipFile Z = null;

        public static Canvas CBlack;

        public static Texture TBlack;

        public static Canvas CGreen;

        public static Texture TGreen;

        public static Canvas CBar;

        public static Texture TBar;

        public static Texture TitleText;

        public static Texture ProgressText;

        public static DNet D;

        public static int Count = 0;

        public static int TotalCount = 0;

        public static long Percent;

        public static string FilePath = "";

        public static string[] FileList;

        public static ContentReturn Initialize()
        {
            CGreen = new Canvas(0, 96, 0, 255);
            CBlack = new Canvas(0, 0, 0, 255);
            CBar = new Canvas(0, 96, 0, 255);
            FilePath = Path.GetTempPath() + "\\Lightness_Updater_" + ProductSettings.Version.ApplicationID + ".bin";
            FileList = new string[1024];
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            Texture.SetFont("Meiryo");
            Texture.SetTextSize(32);
            Texture.SetTextColor(255, 255, 255);
            TitleText = Texture.CreateFromText("オンライン アップデート");
            Texture.SetTextSize(24);
            ProgressText = Texture.CreateFromText("Please wait...");
            CGreen.Box(0, 0, 904, 29);
            TGreen = CGreen.ToTexture();
            CBlack.Rectangle(20, 20, 20, 20);
            TBlack = CBlack.ToTexture();
            CBar.Rectangle(0, 0, LECommon.WindowW, LECommon.WindowH);
            TBar = CBar.ToTexture();
            NowState = State.Auth;
            return ContentReturn.OK;
        }

        public static ContentReturn Process()
        {
            switch (NowState)
            {
                case State.Auth:
                    {
                        ProgressText = Texture.CreateFromText(string.Format("認証中..."));
                        Lightness.Debug.Log("Checking new version...");
                        DNet dNet = new DNet("http://DNGSS.update.network.dark-x.net/Version.txt");
                        string[] strings = dNet.GetStrings();
                        VersionInfo versionInfo;
                        try
                        {
                            versionInfo = new VersionInfo(strings[0]);
                        }
                        catch
                        {
                            Scene.Set("Advertise");
                            return ContentReturn.Change;
                        }
                        ulong result = 0uL;
                        ulong result2 = 0uL;
                        ulong.TryParse(ProductSettings.Version.Date + ProductSettings.Version.Count, out result);
                        ulong.TryParse(versionInfo.Date + versionInfo.Count, out result2);
                        Lightness.Debug.Log('I', "Online Update (Temp)", "Current: {0}", result);
                        Lightness.Debug.Log('I', "Online Update (Temp)", "Server : {0}", result2);
                        if (result >= result2)
                        {
                            Scene.Set("Advertise");
                            return ContentReturn.Change;
                        }
                        D = new DNet("http://DNGSS.update.network.dark-x.net/DNGSS.zip");
                        NowState = State.Download;
                        break;
                    }
                case State.Download:
                    Percent = (long)D.SFEXReadTotal * 1000L / D.FileSize;
                    ProgressText = Texture.CreateFromText(string.Format("ダウンロードしています... {0}/{1}\u3000", D.SFEXReadTotal, D.FileSize, Percent));
                    if (D.SaveFileEx(FilePath) == ContentReturn.End)
                    {
                        Percent = 1000L;
                        NowState = State.Verify;
                    }
                    break;
                case State.Verify:
                    {
                        ProgressText = Texture.CreateFromText(string.Format("確認中..."));
                        ReadOptions readOptions = new ReadOptions();
                        readOptions.Encoding = Encoding.GetEncoding("shift_jis");
                        try
                        {
                            Z = ZipFile.Read(FilePath, readOptions);
                        }
                        catch
                        {
                            Scene.Set("Advertise");
                            return ContentReturn.Change;
                        }
                        Z.Password = "DNGSS";
                        Z.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                        foreach (string entryFileName in Z.EntryFileNames)
                        {
                            FileList[TotalCount] = entryFileName;
                            TotalCount++;
                        }
                        NowState = State.Update;
                        Percent = 0L;
                        break;
                    }
                case State.Update:
                    {
                        try
                        {
                            Percent = Count * 1000 / TotalCount;
                        }
                        catch
                        {
                        }
                        ProgressText = Texture.CreateFromText(string.Format("更新しています... {0}/{1}\u3000", Count + 1, TotalCount));
                        ZipEntry zipEntry = Z[FileList[Count]];
                        zipEntry.Extract(".", ExtractExistingFileAction.OverwriteSilently);
                        Count++;
                        if (Count == TotalCount)
                        {
                            NowState = State.Restart;
                            Percent = 1000L;
                            TotalCount = 60;
                            Count = 0;
                        }
                        break;
                    }
                case State.Restart:
                    {
                        ProgressText = Texture.CreateFromText(string.Format("完了しました。再起動します...\u3000"));
                        Process process = new Process();
                        process.StartInfo.FileName = Assembly.GetEntryAssembly().Location;
                        process.StartInfo.Arguments = "";
                        process.Start();
                        return ContentReturn.End;
                    }
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            Graphic.Draw(TBlack, 0, 0);
            Graphic.Draw(TGreen, 28, 268);
            Graphic.Draw(TitleText, 20, 20);
            Graphic.Draw(ProgressText, 30, 220);
            Graphic.DrawEx(TBar, 30, 270, 255, 0, 0, 900 * (int)Percent / 1000, 22);
            return ContentReturn.OK;
        }
    }
}
