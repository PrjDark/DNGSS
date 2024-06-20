using LEScripts;
using Lightness.Media;

namespace LESFunction
{
    public static class SE
    {
        public static string Play(string[] Args)
        {
            Lightness.Media.SE sE = new Lightness.Media.SE();
            sE.LoadFile(Args[0] + ".wav");
            object scriptMissingLog = ParseTest.ScriptMissingLog;
            ParseTest.ScriptMissingLog = string.Concat(scriptMissingLog, "  SE : ", Args[0], " (", ParseTest.File, ", ", ParseTest.Line, ")\n");
            sE.Play();
            return "";
        }
    }
}
