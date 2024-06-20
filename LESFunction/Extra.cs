using LEScripts;

namespace LESFunction
{
    public static class Extra
    {
        public static string Start3Screen(string[] Args)
        {
            ParseTest.EnableExtraScreen = true;
            return "";
        }

        public static string SetRenderTarget(string[] Args)
        {
            if (Args[0] == "L")
            {
                ParseTest.ESTarget = ParseTest.ESTargets.L;
            }
            if (Args[0] == "C")
            {
                ParseTest.ESTarget = ParseTest.ESTargets.C;
            }
            if (Args[0] == "R")
            {
                ParseTest.ESTarget = ParseTest.ESTargets.R;
            }
            return "";
        }

        public static string End3Screen(string[] Args)
        {
            ParseTest.EnableExtraScreen = false;
            return "";
        }

        public static string SetScreenPosition(string[] Args)
        {
            return "";
        }
    }
}
