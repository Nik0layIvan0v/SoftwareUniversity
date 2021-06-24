using BattleCards.Models;

namespace BattleCards.Services
{
    public interface IUserService
    {
        bool IsUsernameAlreadyRegistered(string username);

        string GetUserId(LoginUserInputModel inputModel);

        void CreateUser(RegisterUserInputModel inputModel);
    }
}