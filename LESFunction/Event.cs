using LEScripts;
using Lightness;
using Lightness.Graphic;

namespace LESFunction
{
    public static class Event
    {
        public static string Set(string[] Args)
        {
            try
            {
                ParseTest.Event = Texture.CreateFromFile(Args[0] + ".png");
            }
            catch
            {
                Debug.Log('E', "Script", "イベントCGデータがありません: {0}", Args[0]);
                object scriptMissingLog = ParseTest.ScriptMissingLog;
                ParseTest.ScriptMissingLog = string.Concat(scriptMissingLog, "Event: ", Args[0], " (", ParseTest.File, ", ", ParseTest.Line, ")\n");
                ParseTest.Event = Texture.CreateFromText("\u3000");
            }
            return "";
        }

        public static string Clear(string[] Args)
        {
            ParseTest.Event = Texture.CreateFromText(" ");
            return "";
        }
    }
}
