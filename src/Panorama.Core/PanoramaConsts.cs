using Panorama.Debugging;

namespace Panorama
{
    public class PanoramaConsts
    {
        public const string LocalizationSourceName = "Panorama";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "9c060073dfdc4e9ab5621c49ae4adbd0";

        #region Custom Constants

        public const int MaxCorrelationIdLength = 64;

        #endregion
    }
}
