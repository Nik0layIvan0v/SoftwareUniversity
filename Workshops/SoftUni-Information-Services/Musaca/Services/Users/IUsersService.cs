using Musaca.Data.EntityModels;
using Musaca.Models.UserModels;

namespace Musaca.Services.Users
{
    public interface IUsersService
    {
        void CreateUser(RegisterUserInputModel inputModel);

        bool IsUsernameRegister(string username);

        bool IsEmailRegister(string email);

        User GetUserFromDb(LoginUserInputModel inputModel);
    }
}