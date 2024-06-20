using dNetwork;

namespace LEScripts
{
    public static class ProductSettings
    {
        public static VersionInfo Version;

        static ProductSettings()
        {
            Version = new VersionInfo("DNGSS:*:*:B:20191231001", "春の花火");
        }
    }
}
