using System.Globalization;
using Lightness;
using Lightness.Engine;

namespace LEScripts
{
    public static class Startup
    {
        public static ContentReturn Process()
        {
            Common.GameVersion = ProductSettings.Version;
            Common.GameVersion.RegionCode = "W";
            try
            {
                if (CultureInfo.CurrentUICulture.Name == "ja-JP")
                {
                    Common.GameVersion.RegionCode = "J";
                }
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    Common.GameVersion.RegionCode = "E";
                }
                if (CultureInfo.CurrentUICulture.Name == "en-US")
                {
                    Common.GameVersion.RegionCode = "U";
                }
            }
            catch
            {
            }
            if (LECommon.GEngine.GraphicType == "DirectX9_SM3")
            {
                Common.GameVersion.DeviceCode = "A";
            }
            if (LECommon.GEngine.GraphicType == "DirectX9_SM2")
            {
                Common.GameVersion.DeviceCode = "B";
            }
            if (LECommon.GEngine.GraphicType == "Software")
            {
                Common.GameVersion.DeviceCode = "C";
            }
            Settings.SetTitle(Common.GameVersion);
            Scene.Set("OnlineUpdate");
            return ContentReturn.Change;
        }
    }
}
