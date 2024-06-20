using Lightness;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;

namespace LEScripts
{
    public static class Config
    {
        private static Texture ConfigBG;

        private static Texture VersionText;

        private static Texture MenuTitleText;

        private static PointerMenu ConfigMenu;

        private static bool First = false;

        public static ContentReturn Initialize()
        {
            AutoSceneFadeHelper.ResetB();
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            ConfigBG = Texture.CreateFromFile("ConfigBG.png");
            Texture.SetFont("Consolas");
            Texture.SetTextSize(20);
            Texture.SetTextColor(255, 255, 255);
            VersionText = Texture.CreateFromText(Common.GameVersion);
            Texture.SetFont("Meiryo");
            Texture.SetTextSize(32);
            MenuTitleText = Texture.CreateFromText("設定");
            ConfigMenu = new PointerMenu();
            ConfigMenu.SetPointer("Pointer.png");
            ConfigMenu.SetSE("Menu.wav", "DNGOut.wav");
            ConfigMenu.SetFont("Meiryo");
            ConfigMenu.SetSize(22);
            ConfigMenu.SetColor(255, 255, 255);
            ConfigMenu.Add("アップデートの確認", 40);
            ConfigMenu.Add("サークル Webサイトへ", 40);
            ConfigMenu.Add("サークル Twitter @PrjDark", 41);
            ConfigMenu.Add("もどる", 99);
            return ContentReturn.End;
        }

        public static ContentReturn Process()
        {
            if (!First)
            {
                First = true;
            }
            if (ConfigMenu.Process() == ContentReturn.End)
            {
                if (ConfigMenu.Selected.ID == 40)
                {
                    Web.OpenBrowser("http://c.dark-x.net/");
                    ConfigMenu.Disabled = false;
                }
                if (ConfigMenu.Selected.ID == 41)
                {
                    Web.OpenBrowser("http://c.dark-x.net/Twitter");
                    ConfigMenu.Disabled = false;
                }
                if (ConfigMenu.Selected.ID == 99)
                {
                    AutoSceneFadeHelper.NowFadeOut = true;
                }
            }
            if (AutoSceneFadeHelper.Process() == ContentReturn.End)
            {
                Scene.Set("MainTitle");
                return ContentReturn.Change;
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            Graphic.Draw(ConfigBG, 0, 0, 255);
            Graphic.Draw(MenuTitleText, 20, 20, 255);
            Graphic.Draw(VersionText, 20, 80, 255);
            PointerMenu.DrawLastProced(320, 120);
            return ContentReturn.OK;
        }
    }
}
