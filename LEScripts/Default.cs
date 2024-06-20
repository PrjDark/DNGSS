using Lightness;
using Lightness.Framework;
using Lightness.Graphic;

namespace LEScripts
{
    public static class Default
    {
        public static Texture LoadingAnime;

        public static ContentReturn Initialize()
        {
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            return ContentReturn.OK;
        }

        public static ContentReturn DrawInLoading()
        {
            if (LoadingAnime == null)
            {
                LoadingAnime = Texture.CreateFromFile("Loading.png");
                LoadingAnime.SetAnimate(320, 160, 4);
            }
            Graphic.Draw(LoadingAnime, 600, 360, 255);
            return ContentReturn.OK;
        }

        public static ContentReturn Process()
        {
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            return ContentReturn.OK;
        }
    }
}
