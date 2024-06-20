using LEScripts;
using Lightness;
using Lightness.Graphic;

namespace LESFunction
{
    public static class BG
    {
        public static string Set(string[] Args)
        {
            try
            {
                if (ParseTest.EnableExtraScreen)
                {
                    switch (ParseTest.ESTarget)
                    {
                        case ParseTest.ESTargets.L:
                            ParseTest.BEffect.Texture = Texture.CreateFromFile(Args[0] + ".png").GetTexture2D_Unsupported();
                            break;
                        case ParseTest.ESTargets.R:
                            ParseTest.BEffect2.Texture = Texture.CreateFromFile(Args[0] + ".png").GetTexture2D_Unsupported();
                            break;
                        case ParseTest.ESTargets.C:
                            ParseTest.BEffect3.Texture = Texture.CreateFromFile(Args[0] + ".png").GetTexture2D_Unsupported();
                            break;
                    }
                    ParseTest.BG = Texture.CreateFromFile(Args[0] + ".png");
                }
                else
                {
                    ParseTest.BG = Texture.CreateFromFile(Args[0] + ".png");
                }
            }
            catch
            {
                Debug.Log('E', "Script", "背景データがありません: {0}", Args[0]);
                object scriptMissingLog = ParseTest.ScriptMissingLog;
                ParseTest.ScriptMissingLog = string.Concat(scriptMissingLog, " BG  : ", Args[0], " (", ParseTest.File, ", ", ParseTest.Line, ")\n");
            }
            return "";
        }

        public static string Clear(string[] Args)
        {
            ParseTest.BG = Texture.CreateFromText(" ");
            return "";
        }
    }
}
