namespace Core.IdentityServer.Constants
{
    public static class ValidationConstants
    {
        // Validation Constants
        public static class Validation
        {
            public const string EmailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 50;
            public const string InvalidEmail = "Invalid Email";
            public const string PasswordTooShort = "Password Too Short";
            public const string PasswordTooLong = "Password Too Long";
        }
    }
}
