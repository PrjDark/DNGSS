using Lightness;
using Lightness.Engine;
using Lightness.Framework;
using Lightness.Graphic;

namespace LEScripts
{
    public static class Advertise
    {
        private static Texture Loading;

        private static Texture WarnMsg;

        private static Texture LogoTeamDango;

        private static Texture LogoPrjDark;

        private static Texture LogoDNetwork;

        public static ContentReturn Initialize()
        {
            return ContentReturn.OK;
        }

        public static ContentReturn Loader()
        {
            WarnMsg = Texture.CreateFromFile("WarnMsg.png");
            LogoTeamDango = Texture.CreateFromFile("LogoTeamDango.png");
            LogoPrjDark = Texture.CreateFromFile("LogoProjectDark.png");
            LogoDNetwork = Texture.CreateFromFile("LogoDNetwork.png");
            AdvertiseHelper.ResetB(20, 90);
            AdvertiseHelper.AddTexture(WarnMsg);
            AdvertiseHelper.AddTexture(LogoPrjDark);
            AdvertiseHelper.AddTexture(LogoTeamDango);
            AdvertiseHelper.AddTexture(LogoDNetwork);
            return ContentReturn.End;
        }

        public static ContentReturn Process()
        {
            if (AdvertiseHelper.Process() == ContentReturn.End)
            {
                Scene.Set("MainTitle");
                return ContentReturn.Change;
            }
            return ContentReturn.OK;
        }

        public static ContentReturn Draw()
        {
            AdvertiseHelper.Draw();
            return ContentReturn.OK;
        }
    }
}
