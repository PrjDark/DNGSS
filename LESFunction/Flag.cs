using LEScripts;

namespace LESFunction
{
    public static class Flag
    {
        public static string GotoIf(string[] Args)
        {
            if (ParseTest.Flag[0] && ParseTest.Flag[1])
            {
                return Scene.Goto(Args);
            }
            return "";
        }

        public static string CallIf(string[] Args)
        {
            if (ParseTest.Flag[0] && ParseTest.Flag[1])
            {
                return Scene.Call(Args);
            }
            return "";
        }

        public static string Set(string[] Args)
        {
            if (Args[0] == "0")
            {
                ParseTest.Flag[0] = true;
            }
            if (Args[0] == "1")
            {
                ParseTest.Flag[1] = true;
            }
            return "";
        }

        public static string Clear(string[] Args)
        {
            if (Args[0] == "0")
            {
                ParseTest.Flag[0] = false;
            }
            if (Args[0] == "1")
            {
                ParseTest.Flag[1] = false;
            }
            return "";
        }
    }
}
