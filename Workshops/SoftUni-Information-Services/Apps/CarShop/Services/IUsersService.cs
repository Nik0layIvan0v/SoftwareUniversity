using CarShop.ViewModels.Users;

namespace CarShop.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Create(RegisterInputUserModel inputUserModel);

        bool IsUsernameAvailable(string username);

        public bool IsUserMechanic(string Userid);
    }
}
