using Lightness;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;

namespace LEScripts
{
    public static class RatingInfo
    {
        private static Texture RatingMsg;

        public static ContentReturn Initialize()
        {
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            RatingMsg = Texture.CreateFromFile("RatingMsg.png");
            AdvertiseHelper.ResetB(20, 100);
            AdvertiseHelper.AddTexture(RatingMsg);
            return ContentReturn.End;
        }

        public static ContentReturn Process()
        {
            if (AdvertiseHelper.Process() == ContentReturn.End)
            {
                Scene.Set("Advertise");
                return ContentReturn.Change;
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            AdvertiseHelper.Draw();
            return ContentReturn.OK;
        }

        public static ContentReturn DrawInLoading()
        {
            return ContentReturn.OK;
        }
    }
}
