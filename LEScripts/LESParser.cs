using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using LESFunction;
using Lightness;
using Lightness.Engine;

namespace LEScripts
{
    public class LESParser
    {
        public enum TokenType
        {
            None,
            GroupStart,
            GroupEnd,
            Separate,
            Equal,
            Add,
            Sub,
            Mul,
            Div,
            Not,
            Larger,
            Smaller,
            End,
            BlockStart,
            BlockEnd,
            Space
        }

        public ArrayList Token;

        protected int P;

        protected int LastSeparated;

        public LESParser(string s)
        {
            Token = new ArrayList();
            P = 0;
            SplitByToken(s);
        }

        public TokenType DetectTokenType(char c)
        {
            switch (c)
            {
                case '(':
                    return TokenType.GroupStart;
                case ')':
                    return TokenType.GroupEnd;
                case ',':
                    return TokenType.Separate;
                case '=':
                    return TokenType.Equal;
                case '+':
                    return TokenType.Add;
                case '-':
                    return TokenType.Sub;
                case '*':
                    return TokenType.Mul;
                case '/':
                    return TokenType.Div;
                case '!':
                    return TokenType.Not;
                case '<':
                    return TokenType.Larger;
                case '>':
                    return TokenType.Smaller;
                case ';':
                    return TokenType.End;
                case '{':
                    return TokenType.BlockStart;
                case '}':
                    return TokenType.BlockEnd;
                case '\t':
                case '\n':
                case '\r':
                case ' ':
                    return TokenType.Space;
                default:
                    return TokenType.None;
            }
        }

        public bool SplitByToken(string text)
        {
            bool flag = false;
            bool flag2 = false;
            int num = 0;
            char c = '\0';
            TokenType tokenType = TokenType.None;
            for (int i = 0; i < text.Length; i++)
            {
                c = text[i];
                if (flag2)
                {
                    if (c == '\n')
                    {
                        flag2 = false;
                    }
                    continue;
                }
                char c2 = c;
                if (c2 == '"' && text[i - 1] != '\\')
                {
                    if (flag)
                    {
                        Token.Add(text.Substring(num, i - num + 1));
                        num = i + 1;
                        flag = false;
                    }
                    else
                    {
                        num = i;
                        flag = true;
                    }
                }
                if (flag || (tokenType = DetectTokenType(c)) == TokenType.None)
                {
                    continue;
                }
                if (c == '/' && text[i + 1] == '/')
                {
                    flag2 = true;
                    continue;
                }
                if (i - num != 0)
                {
                    string text2 = text.Substring(num, i - num);
                    if (text2.Length != 0)
                    {
                        Token.Add(text2);
                    }
                }
                if (tokenType != TokenType.Space)
                {
                    Token.Add(string.Concat(c));
                }
                num = i + 1;
            }
            Token.Add(text.Substring(num));
            return true;
        }

        public string Exec()
        {
            return Separate();
        }

        public string Separate()
        {
            string result = Reversal();
            while ((string)Token[P] == ",")
            {
                LastSeparated = P;
                P++;
                result = Reversal();
            }
            return result;
        }

        public string Reversal()
        {
            return Equal();
        }

        public string Equal()
        {
            return AddSub();
        }

        public string AddSub()
        {
            return MulDiv();
        }

        public string MulDiv()
        {
            return Solve();
        }

        public string Solve()
        {
            int num = 0;
            string text = "";
            if ((string)Token[P + 1] == "(")
            {
                num = P;
                if ((string)Token[P + 2] == ")")
                {
                    P += 2;
                }
                else
                {
                    P += 2;
                    Exec();
                }
                P++;
                string text2 = (string)Token[num];
                ArrayList arrayList = new ArrayList();
                for (int i = num + 2; i < P - 1; i += 2)
                {
                    arrayList.Add(Token[i]);
                }
                for (int j = num; j < P; j++)
                {
                    Token.RemoveAt(num);
                }
                P -= P - num;
                int num2 = text2.LastIndexOf('.');
                string text3;
                string text4;
                if (num2 != -1)
                {
                    text3 = text2.Substring(0, num2);
                    text4 = text2.Substring(num2 + 1);
                }
                else
                {
                    text3 = text2;
                    text4 = "Default";
                }
                Debug.Log("{0} / {1}", text3, text4);
                try
                {
                    Assembly executingAssembly = Assembly.GetExecutingAssembly();
                    FunctionEntry functionEntry = (FunctionEntry)Delegate.CreateDelegate(typeof(FunctionEntry), executingAssembly.GetType("LESFunction." + text3).GetMethod(text4));
                    text = functionEntry((string[])arrayList.ToArray(typeof(string)));
                }
                catch (ArgumentNullException)
                {
                    LECommon.MainWindow.Hide();
                    MessageBox.Show("関数の呼び出しに失敗しました: " + text2, LECommon.EngineVer.Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    LECommon.MainWindow.Show();
                }
                Token.Insert(P, text);
                P++;
                return text;
            }
            if (((string)Token[P])[0] == '"')
            {
                string text5 = (string)Token[P];
                text5 = text5.Substring(1, text5.Length - 2);
                Token.RemoveAt(P);
                Token.Insert(P, text5);
                P++;
                return text5;
            }
            if ('0' <= ((string)Token[P])[0] && ((string)Token[P])[0] <= '9')
            {
                P++;
                return (string)Token[P - 1];
            }
            Debug.Log("*NOT_IMPLEMENTED* Add straight. ({0})", Token[P]);
            P++;
            return (string)Token[P - 1];
        }
    }
}
