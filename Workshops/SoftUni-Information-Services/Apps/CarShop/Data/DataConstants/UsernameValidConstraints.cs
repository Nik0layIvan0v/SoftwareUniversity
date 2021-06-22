namespace CarShop.Data.DataConstants
{
    public class UsernameValidConstraints
    {
        public const int MinUsernameLength = 4;

        public const int DefaultMaxLength = 20;

        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public const int PasswordMinLength = 5;
    }
}