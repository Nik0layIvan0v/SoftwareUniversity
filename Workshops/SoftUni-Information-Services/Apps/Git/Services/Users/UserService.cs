using System.Linq;
using System.Text;
using Git.Data;
using Git.Data.Models;

namespace Git.Services.Users
{
    public class UserService : IUsersService
    {
        private protected readonly ApplicationDbContext DbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public string CreateUser(string username, string email, string password)
        {
            User user = new User
            {
                Username = username,
                Email = email,
                Password = HashInputPassword(password),
            };

            this.DbContext.Users.Add(user);

            this.DbContext.SaveChanges();

            return user.Id; //WHAT TO RTURn?
        }

        public bool IsEmailAvailable(string email)
        {
            return this.DbContext.Users.Any(x => x.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            return this.DbContext.Users.FirstOrDefault(x => 
                x.Username == username && 
                x.Password == HashInputPassword(password))
                ?.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.DbContext.Users.Any(x => x.Username == username);
        }

        private static string HashInputPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
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
}