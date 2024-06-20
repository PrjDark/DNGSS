using LEScripts;
using Lightness.Framework;
using Lightness.Graphic;

namespace LESFunction
{
    public static class UserInputs
    {
        public static string If(string[] Args)
        {
            ParseTest.UserMenu = new PointerMenu();
            ParseTest.UserMenu.SetPointer("Pointer.png");
            ParseTest.UserMenu.SetSE("Menu.wav", "DNGOut.wav");
            ParseTest.UserMenu.SetFont("Meiryo");
            ParseTest.UserMenu.SetSize(24);
            ParseTest.UserMenu.SetColor(255, 255, 255);
            try
            {
                ParseTest.Text = Texture.CreateFromText(Args[0]);
            }
            catch
            {
            }
            for (int i = 1; i < Args.Length; i++)
            {
                ParseTest.UserMenu.Add(Args[i], i);
            }
            ParseTest.MenuMode = true;
            return "";
        }

        public static string GotoIf(string[] Args)
        {
            if (Args[2] == ParseTest.LastIf.ToString())
            {
                return Scene.Goto(Args);
            }
            return "";
        }

        public static string CallIf(string[] Args)
        {
            if (Args[2] == ParseTest.LastIf.ToString())
            {
                return Scene.Call(Args);
            }
            return "";
        }

        public static string HideSystem(string[] Args)
        {
            ParseTest.HideSystemWin = true;
            return "";
        }
    }
}
