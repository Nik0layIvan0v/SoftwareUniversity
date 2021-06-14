namespace MyFirstMvcApp.Services
{
    public interface IUsersService
    {
        void CreateUser(string username, string password, string email);

        bool IsUserValid(string username, string password);

        bool IsUserAlreadyRegistered(string username);

        bool IsEmailAlreadyRegistered(string email);
    }
}