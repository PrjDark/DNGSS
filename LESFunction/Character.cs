using LEScripts;
using Lightness;
using Lightness.Graphic;

namespace LESFunction
{
    public static class Character
    {
        public static string Add(string[] Args)
        {
            for (int i = 0; i < ParseTest.CharData.Length; i++)
            {
                if (!(ParseTest.CharData[i].T == null))
                {
                    continue;
                }
                try
                {
                    ParseTest.CharData[i].T = Texture.CreateFromFile(Args[0] + ".png");
                    switch (Args[1])
                    {
                        case "L":
                            ParseTest.CharData[i].Pos = ParseTest.ESTargets.L;
                            break;
                        case "R":
                            ParseTest.CharData[i].Pos = ParseTest.ESTargets.R;
                            break;
                        case "C":
                            ParseTest.CharData[i].Pos = ParseTest.ESTargets.C;
                            break;
                        default:
                            ParseTest.CharData[i].Pos = ParseTest.ESTargets.C;
                            break;
                    }
                    ParseTest.CharData[i].ID = Args[3];
                }
                catch
                {
                    Debug.Log('E', "Script", "Add: キャラクターデータがありません: {0}", Args[0]);
                    object scriptMissingLog = ParseTest.ScriptMissingLog;
                    ParseTest.ScriptMissingLog = string.Concat(scriptMissingLog, "ChrAd: ", Args[0], " (", ParseTest.File, ", ", ParseTest.Line, ")\n");
                }
                break;
            }
            return "";
        }

        public static string Change(string[] Args)
        {
            for (int i = 0; i < ParseTest.CharData.Length; i++)
            {
                if (ParseTest.CharData[i].T != null && ParseTest.CharData[i].ID == Args[0])
                {
                    try
                    {
                        ParseTest.CharData[i].T = Texture.CreateFromFile(Args[1] + ".png");
                    }
                    catch
                    {
                        Debug.Log('E', "Script", "Change: キャラクターデータがありません: {0}", Args[1]);
                        object scriptMissingLog = ParseTest.ScriptMissingLog;
                        ParseTest.ScriptMissingLog = string.Concat(scriptMissingLog, "Chang: ", Args[1], " (", ParseTest.File, ", ", ParseTest.Line, ")\n");
                    }
                    break;
                }
            }
            return "";
        }

        public static string Remove(string[] Args)
        {
            for (int i = 0; i < ParseTest.CharData.Length; i++)
            {
                if (ParseTest.CharData[i].T != null && ParseTest.CharData[i].ID == Args[0])
                {
                    ParseTest.CharData[i].T = null;
                    ParseTest.CharData[i].ID = "?";
                    break;
                }
            }
            return "";
        }

        public static string Clear(string[] Args)
        {
            for (int i = 0; i < ParseTest.CharData.Length; i++)
            {
                ParseTest.CharData[i].T = null;
                ParseTest.CharData[i].ID = "?";
            }
            return "";
        }
    }
}
