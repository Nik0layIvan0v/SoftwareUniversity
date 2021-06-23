using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Users;

namespace CarShop.Services
{
    public class UsersService: IUsersService
    {
        private protected readonly ApplicationDbContext DbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void Create(RegisterInputUserModel model)
        {
            User user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = GetHashedPassword(model.Password),
                IsMechanic = model.UserType == "Mechanic"
            };

            this.DbContext.Users.Add(user);
            this.DbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            return this.DbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == GetHashedPassword(password))?.Id;
        }

        public bool IsUserMechanic(string Userid)
        {
            User user = this.DbContext.Users.FirstOrDefault(u => u.Id == Userid);

            if (user != null)
            {
                return user.IsMechanic;
            }

            return false;
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.DbContext.Users.Any(user => user.Username.ToLower() == username.ToLower());
        }

        public static string GetHashedPassword(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using var hash = SHA512.Create();

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
