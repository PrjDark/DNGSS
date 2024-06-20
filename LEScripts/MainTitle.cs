using Lightness;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.Media;

namespace LEScripts
{
    public static class MainTitle
    {
        private enum MenuType
        {
            Title,
            Story,
            Character
        }

        private static MenuType CurrentMenu;

        private static BGM TitleBGM;

        private static Texture TitleBG;

        private static Texture VersionText;

        private static PointerMenu MainMenu;

        private static int TimeCount = 0;

        private static int LastMainMenuPos = 0;

        private static bool First = false;

        public static ContentReturn Initialize()
        {
            AutoSceneFadeHelper.ResetB();
            CurrentMenu = MenuType.Title;
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            TitleBG = Texture.CreateFromFile("TitleBG.png");
            Texture.SetFont("Consolas");
            Texture.SetTextSize(20);
            Texture.SetTextColor(255, 255, 255);
            VersionText = Texture.CreateFromText(Common.GameVersion);
            TitleBGM = new BGM();
            TitleBGM.LoadFile("BGM_Title.wav");
            MainMenu = new PointerMenu();
            MainMenu.SetPointer("Pointer.png");
            MainMenu.SetSE("Menu.wav", "DNGOut.wav");
            MainMenu.SetFont("Meiryo");
            MainMenu.SetSize(24);
            MainMenu.SetColor(255, 255, 255);
            MainMenu.Add("はじめから", 10);
            MainMenu.Add("設定", 80);
            MainMenu.Add("終了", 99);
            First = false;
            TimeCount = 0;
            LastMainMenuPos = 0;
            return ContentReturn.End;
        }

        public static ContentReturn Process()
        {
            if (!First)
            {
                TitleBGM.Play();
                First = true;
            }
            AutoSceneFadeHelper.Process();
            if (MainMenu.Process() == ContentReturn.End)
            {
                if (AutoSceneFadeHelper.LastStatus == ContentReturn.End)
                {
                    Scene.Set("ParseTest");
                    if (MainMenu.Selected.ID == 10)
                    {
                        Scene.Set("ParseTest");
                    }
                    if (MainMenu.Selected.ID == 80)
                    {
                        Scene.Set("Config");
                    }
                    if (MainMenu.Selected.ID == 99)
                    {
                        return ContentReturn.End;
                    }
                    return ContentReturn.Change;
                }
                AutoSceneFadeHelper.NowFadeOut = true;
            }
            else
            {
                TimeCount++;
                if (TimeCount * 33 > TitleBGM.GetLength())
                {
                    if (!AutoSceneFadeHelper.NowFadeOut)
                    {
                        AutoSceneFadeHelper.NowFadeOut = true;
                        TitleBGM.Stop();
                    }
                    if (AutoSceneFadeHelper.LastStatus == ContentReturn.End)
                    {
                        Scene.Set("Advertise");
                        return ContentReturn.Change;
                    }
                }
                else if (LastMainMenuPos != MainMenu.Selected.Order)
                {
                    TimeCount = 0;
                    LastMainMenuPos = MainMenu.Selected.Order;
                }
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            Graphic.Draw(TitleBG, 0, 0, 255);
            Graphic.Draw(VersionText, 10, 10, 255);
            PointerMenu.DrawLastProced(440, 300);
            return ContentReturn.OK;
        }
    }
}
