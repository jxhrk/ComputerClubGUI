namespace CompClubGUICore
{
    public static partial class AppInfo
    {
        public static bool IsThemeDark;

        public static string? SessionToken;

        public static bool AUTOLOGIN_DEBUG = false;

        public static double ScalingFactorMultiplier = 1.3;

        public static bool IsLoggedIn()
        {
            return SessionToken != null || AUTOLOGIN_DEBUG;
        }
    }
}
