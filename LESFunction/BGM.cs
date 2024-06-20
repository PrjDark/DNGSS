using Lightness.Media;

namespace LESFunction
{
    public static class BGM
    {
        public static Lightness.Media.BGM LEBGM;

        public static string Play(string[] Args)
        {
            if (LEBGM == null)
            {
                LEBGM = new Lightness.Media.BGM();
            }
            LEBGM.Close();
            LEBGM.LoadFile(Args[0] + ".wav");
            LEBGM.Play();
            return "";
        }

        public static string Stop(string[] Args)
        {
            if (LEBGM == null)
            {
                LEBGM = new Lightness.Media.BGM();
            }
            LEBGM.Stop();
            return "";
        }
    }
}
