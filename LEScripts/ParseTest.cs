using System.Collections;
using System.IO;
using Lightness;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LEScripts
{
    public class ParseTest
    {
        public struct ReturnStack
        {
            public string File;

            public int Line;
        }

        public struct CharInfo
        {
            public string ID;

            public Lightness.Graphic.Texture T;

            public ESTargets Pos;
        }

        public enum ESTargets
        {
            L,
            R,
            C
        }

        public static CharInfo[] CharData;

        public static bool EnableExtraScreen;

        public static ESTargets ESTarget;

        public static BasicEffect BEffect;

        public static BasicEffect BEffect2;

        public static BasicEffect BEffect3;

        public static RenderTarget2D ExtraScreen;

        public static Vector2 DrawPosition;

        public static Matrix MProjection;

        public static VertexPositionColorTexture[] ExtraScreenV1;

        public static VertexPositionColorTexture[] ExtraScreenV2;

        public static VertexPositionColorTexture[] ExtraScreenV3;

        public static VertexPositionColorTexture[] ExtraScreenV4;

        public static Lightness.Graphic.Texture Text;

        public static Lightness.Graphic.Texture BG;

        public static Lightness.Graphic.Texture Event;

        public static Lightness.Graphic.Texture SystemWindow;

        public static string ScriptLog;

        public static string ScriptMissingLog;

        public static string LastText;

        public static int Line;

        public static string File;

        protected static string LastFile;

        public static string[] Script;

        public static bool ReadNextLine;

        public static bool HideSystemWin;

        public static bool MenuMode;

        public static PointerMenu UserMenu;

        public static int LastIf = 0;

        public static bool[] Flag;

        public static ArrayList Stack;

        public static LESParser LESP;

        public static ContentReturn Initialize()
        {
            HideSystemWin = false;
            MenuMode = false;
            ScriptLog = "";
            ScriptMissingLog = "";
            LastText = "";
            Line = -1;
            File = "Startup";
            LastFile = "*";
            Stack = new ArrayList();
            Lightness.Graphic.Texture.SetFont("Meiryo");
            Lightness.Graphic.Texture.SetTextColor(255, 255, 255);
            Lightness.Graphic.Texture.SetTextSize(24);
            ReadNextLine = true;
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            SystemWindow = Lightness.Graphic.Texture.CreateFromFile("System.png");
            Text = Lightness.Graphic.Texture.CreateFromText("\u3000");
            BG = Lightness.Graphic.Texture.CreateFromText("\u3000");
            Event = Lightness.Graphic.Texture.CreateFromText("\u3000");
            CharData = new CharInfo[4];
            for (int i = 0; i < CharData.Length; i++)
            {
                CharData[i] = default(CharInfo);
                CharData[i].T = null;
            }
            Flag = new bool[16];
            EnableExtraScreen = false;
            ExtraScreen = new RenderTarget2D(LECommon.GEngine.GetGDevice_Unsupported(), LECommon.RenderingW, LECommon.RenderingH);
            DrawPosition = default(Vector2);
            DrawPosition.X = 0f;
            DrawPosition.Y = 0f;
            BEffect = new BasicEffect(LECommon.GEngine.GetGDevice_Unsupported());
            BEffect.TextureEnabled = true;
            MProjection = Matrix.Identity;
            MProjection.M11 = 0.00208333344f;
            MProjection.M22 = -0.00370370364f;
            MProjection.M41 = -1f;
            MProjection.M42 = 1f;
            BEffect.View = Matrix.Identity;
            BEffect.World = Matrix.Identity;
            BEffect.Projection = MProjection;
            BEffect2 = (BasicEffect)BEffect.Clone();
            BEffect3 = (BasicEffect)BEffect.Clone();
            int num = 560;
            int num2 = 400;
            int num3 = 20;
            ExtraScreenV1 = new VertexPositionColorTexture[4];
            ExtraScreenV1[0] = new VertexPositionColorTexture(new Vector3(0f, 0f, 0f), Color.White, new Vector2(0f, 0f));
            ExtraScreenV1[1] = new VertexPositionColorTexture(new Vector3(0f, 540f, 0f), Color.White, new Vector2(0f, 1f));
            ExtraScreenV1[2] = new VertexPositionColorTexture(new Vector3(num, 0f, 0f), Color.White, new Vector2((float)num / 960f, 0f));
            ExtraScreenV1[3] = new VertexPositionColorTexture(new Vector3(num2, 540f, 0f), Color.White, new Vector2((float)num2 / 960f, 1f));
            ExtraScreenV2 = new VertexPositionColorTexture[4];
            ExtraScreenV2[0] = new VertexPositionColorTexture(new Vector3(num, 0f, 0f), Color.White, new Vector2((float)num / 960f, 0f));
            ExtraScreenV2[1] = new VertexPositionColorTexture(new Vector3(num2, 540f, 0f), Color.White, new Vector2((float)num2 / 960f, 1f));
            ExtraScreenV2[2] = new VertexPositionColorTexture(new Vector3(960f, 0f, 0f), Color.White, new Vector2(1f, 0f));
            ExtraScreenV2[3] = new VertexPositionColorTexture(new Vector3(960f, 540f, 0f), Color.White, new Vector2(1f, 1f));
            ExtraScreenV3 = new VertexPositionColorTexture[4];
            ExtraScreenV3[0] = new VertexPositionColorTexture(new Vector3(num - num3, 0f, 0f), Color.White, new Vector2((float)(num - num3) / 960f, 0f));
            ExtraScreenV3[1] = new VertexPositionColorTexture(new Vector3(num2 - num3, 540f, 0f), Color.White, new Vector2((float)(num2 - num3) / 960f, 1f));
            ExtraScreenV3[2] = new VertexPositionColorTexture(new Vector3(num + num3, 0f, 0f), Color.White, new Vector2((float)(num + num3) / 960f, 0f));
            ExtraScreenV3[3] = new VertexPositionColorTexture(new Vector3(num2 + num3, 540f, 0f), Color.White, new Vector2((float)(num2 + num3) / 960f, 1f));
            BEffect.Texture = null;
            BEffect2.Texture = null;
            BEffect3.Texture = null;
            BEffect3.Texture = Lightness.Graphic.Texture.CreateFromFile("DIVBG.png").GetTexture2D_Unsupported();
            return ContentReturn.End;
        }

        public static bool IsScript(string CheckStr)
        {
            int startIndex;
            if ((startIndex = CheckStr.IndexOf('.')) > 0 && (startIndex = CheckStr.IndexOf('(', startIndex)) > 0 && (startIndex = CheckStr.IndexOf(')', startIndex)) > 0 && (startIndex = CheckStr.IndexOf(';', startIndex)) > 0)
            {
                return true;
            }
            return false;
        }

        public static void ExtraScreen_Process()
        {
            if (!EnableExtraScreen)
            {
                return;
            }
            LECommon.GEngine.GetGDevice_Unsupported().RasterizerState = RasterizerState.CullNone;
            LECommon.GEngine.GetGDevice_Unsupported().SetRenderTarget(ExtraScreen);
            LECommon.GEngine.GetGDevice_Unsupported().Clear(ClearOptions.Target, Color.White, 1f, 0);
            foreach (EffectPass pass in BEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                LECommon.GEngine.GetGDevice_Unsupported().DrawUserPrimitives(PrimitiveType.TriangleStrip, ExtraScreenV1, 0, 2);
            }
            foreach (EffectPass pass2 in BEffect2.CurrentTechnique.Passes)
            {
                pass2.Apply();
                LECommon.GEngine.GetGDevice_Unsupported().DrawUserPrimitives(PrimitiveType.TriangleStrip, ExtraScreenV2, 0, 2);
            }
            foreach (EffectPass pass3 in BEffect3.CurrentTechnique.Passes)
            {
                pass3.Apply();
                LECommon.GEngine.GetGDevice_Unsupported().DrawUserPrimitives(PrimitiveType.TriangleStrip, ExtraScreenV3, 0, 2);
            }
        }

        public static ContentReturn Process()
        {
            if (LastFile != File)
            {
                Script = System.IO.File.ReadAllLines("./Contents/Script/" + File + ".txt");
                LastFile = File;
            }
            if (Line >= Script.Length - 1)
            {
                Scene.Set("Advertise");
                return ContentReturn.Change;
            }
            if (MenuMode)
            {
                if (UserMenu.Process() == ContentReturn.End)
                {
                    LastIf = UserMenu.Selected.Order;
                    MenuMode = false;
                    ReadNextLine = true;
                }
                return ContentReturn.OK;
            }
            if (ReadNextLine || IOService.GetInputOnce(0, IOService.InputID.Start) > 0 || IOService.GetInputOnce(0, IOService.InputID.OK) > 0 || IOService.GetPointerInputOnce(0, IOService.PointerInputID.L) > 0)
            {
                ReadNextLine = false;
                Line++;
                if (Script[Line].IndexOf("//") == 0)
                {
                    ReadNextLine = true;
                    return ContentReturn.OK;
                }
                if (Script[Line].IndexOf(':') == Script[Line].Length - 1)
                {
                    ReadNextLine = true;
                    return ContentReturn.OK;
                }
                if (IsScript(Script[Line]))
                {
                    Debug.Log("Script: {0}", Script[Line]);
                    ScriptLog = ScriptLog + "\n" + Script[Line];
                    ReadNextLine = true;
                    LESP = new LESParser(Script[Line]);
                    LESP.Exec();
                    return ContentReturn.OK;
                }
                ExtraScreen_Process();
                if (Script[Line] != "")
                {
                    Text.Close();
                    string text = Script[Line];
                    if (text[text.Length - 1] == '\\')
                    {
                        Line++;
                        text = text + "\n" + Script[Line];
                    }
                    Text = Lightness.Graphic.Texture.CreateFromText(text);
                    return ContentReturn.OK;
                }
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            Graphic.Draw(BG, 0, 0);
            if (EnableExtraScreen)
            {
                LECommon.Sprite.GetRawObject_Unsupported().Draw(ExtraScreen, DrawPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            for (int i = 0; i < CharData.Length; i++)
            {
                if (CharData[i].T != null)
                {
                    switch (CharData[i].Pos)
                    {
                        case ESTargets.C:
                            Graphic.Draw(CharData[i].T, 320, 120);
                            break;
                        case ESTargets.R:
                            Graphic.Draw(CharData[i].T, 580, 120);
                            break;
                        case ESTargets.L:
                            Graphic.Draw(CharData[i].T, 60, 120);
                            break;
                        default:
                            Graphic.Draw(CharData[i].T, 0, 0);
                            break;
                    }
                }
            }
            Graphic.Draw(Event, 0, 0);
            if (!HideSystemWin)
            {
                Graphic.Draw(SystemWindow, 0, 0);
            }
            if (MenuMode)
            {
                Graphic.Draw(Text, 40, 415);
                UserMenu.Draw(100, 450);
            }
            else
            {
                Graphic.Draw(Text, 40, 430);
            }
            return ContentReturn.OK;
        }
    }
}
