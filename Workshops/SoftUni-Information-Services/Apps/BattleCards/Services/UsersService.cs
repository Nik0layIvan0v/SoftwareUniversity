using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BattleCards.Data;
using BattleCards.Data.EntityModels;
using BattleCards.Models;

namespace BattleCards.Services
{
    public class UsersService : IUserService
    {
        private protected readonly ApplicationDbContext DbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public bool IsUsernameAlreadyRegistered(string username)
        {
            return this.DbContext.Users.Any(user => user.Username == username);
        }

        public string GetUserId(LoginUserInputModel model)
        {
            return this.DbContext.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == GetHashedPassword(model.Password))?.Id;
        }

        public void CreateUser(RegisterUserInputModel model)
        {
            User user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = GetHashedPassword(model.Password),
            };

            this.DbContext.Users.Add(user);
            this.DbContext.SaveChanges();
        }

        private static string GetHashedPassword(string inputPassword)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputPassword);

            using var hash = SHA512.Create();

            byte[] hashedInputBytes = hash.ComputeHash(bytes);

            StringBuilder hashedInputStringBuilder = new StringBuilder(128);

            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }

            return hashedInputStringBuilder.ToString();
        }
    }
}