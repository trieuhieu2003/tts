using tts.Debugging;

namespace tts
{
    public class ttsConsts
    {
        public const string LocalizationSourceName = "tts";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "aede053f50f045a6a4ee8bce74ce97de";
    }
}
