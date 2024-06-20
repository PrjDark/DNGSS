using System.IO;
using LEScripts;
using Lightness;
using Lightness.Graphic;

namespace LESFunction
{
    public class Scene
    {
        protected static int SearchLabelLine(string FileName, string LabelName)
        {
            if (FileName == "*")
            {
                FileName = ParseTest.File;
            }
            Debug.Log("*** {0}", "./Contents/Script/" + FileName + ".txt");
            string[] array = File.ReadAllLines("./Contents/Script/" + FileName + ".txt");
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].IndexOf(LabelName + ":") == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string Clear(string[] Args)
        {
            BGM.Stop(Args);
            BG.Clear(Args);
            Character.Clear(Args);
            Event.Clear(Args);
            ParseTest.Text = Texture.CreateFromText(" ");
            return "";
        }

        public static string Reset(string[] Args)
        {
            return Clear(Args);
        }

        public static string Call(string[] Args)
        {
            ParseTest.ReturnStack returnStack = default(ParseTest.ReturnStack);
            returnStack.File = ParseTest.File;
            returnStack.Line = ParseTest.Line;
            ParseTest.Stack.Add(returnStack);
            if (Args[0] != "*")
            {
                ParseTest.File = Args[0];
            }
            if (Args[1] != "0")
            {
                ParseTest.Line = SearchLabelLine(Args[0], Args[1]);
            }
            else
            {
                ParseTest.Line = -1;
            }
            ParseTest.ReadNextLine = true;
            return "";
        }

        public static string Goto(string[] Args)
        {
            if (Args[0] != "*")
            {
                ParseTest.File = Args[0];
            }
            if (Args[1] != "0")
            {
                ParseTest.Line = SearchLabelLine(Args[0], Args[1]);
            }
            else
            {
                ParseTest.Line = -1;
            }
            ParseTest.ReadNextLine = true;
            return "";
        }

        public static string Skip(string[] Args)
        {
            int result = 0;
            int.TryParse(Args[1], out result);
            ParseTest.Line = result - 1;
            return "";
        }

        public static string Return(string[] Args)
        {
            int num = 0;
            Debug.Log("LEScripts.ParseTest.Stack.Count: {0}", ParseTest.Stack.Count);
            if ((num = ParseTest.Stack.Count) != 0)
            {
                num--;
                ParseTest.File = ((ParseTest.ReturnStack)ParseTest.Stack[num]).File;
                ParseTest.Line = ((ParseTest.ReturnStack)ParseTest.Stack[num]).Line;
                ParseTest.Stack.RemoveAt(num);
                ParseTest.ReadNextLine = true;
            }
            return "";
        }
    }
}
