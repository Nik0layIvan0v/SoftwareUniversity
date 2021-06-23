using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Musaca.Data;
using Musaca.Data.EntityModels;
using Musaca.Models.UserModels;
using SUS.MvcFramework;

namespace Musaca.Services.Users
{
    public class UsersService : IUsersService
    {
        private protected readonly MusacaDbContext DbContext;

        public UsersService(MusacaDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void CreateUser(RegisterUserInputModel inputModel)
        {
            User newUser = new User
            {
                Username = inputModel.Username,
                Email = inputModel.Email,
                Role = (IdentityRole)1,
                Password = HashPassword(inputModel.Password)
            };

            this.DbContext.Users.Add(newUser);
            this.DbContext.SaveChanges();
        }

        public bool IsUsernameRegister(string username)
        {
            return this.DbContext.Users.Any(user => user.Username == username);
        }

        public bool IsEmailRegister(string email)
        {
            return this.DbContext.Users.Any(user => user.Email == email);
        }

        public User GetUserFromDb(LoginUserInputModel inputModel)
        {
            return this.DbContext.Users.FirstOrDefault(x =>
                x.Username == inputModel.Username && x.Password == HashPassword(inputModel.Password));
        }

        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}