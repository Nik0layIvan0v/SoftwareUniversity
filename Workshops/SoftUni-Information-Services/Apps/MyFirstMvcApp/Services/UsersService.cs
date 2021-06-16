using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MyFirstMvcApp.Data;
using MyFirstMvcApp.Data.EntityModels;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Services
{
    public class UsersService : IUsersService
    {
        private protected readonly ApplicationDbContext DbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void CreateUser(string username, string password, string email)
        {
            DbContext.Users.Add(new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password),
                Role = (IdentityRole) 0,
                Description = $"User is registered on {DateTime.UtcNow.ToString("f", CultureInfo.InvariantCulture)}"
            });

            DbContext.SaveChanges();
        }

        public bool IsUserValid(string username, string password)
        {
            return DbContext.Users.Any(x => x.Username == username && x.Password == ComputeHash(password));
        }

        public bool IsUserAlreadyRegistered(string username)
        {
            return this.DbContext.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAlreadyRegistered(string email)
        {
            return this.DbContext.Users.Any(x => x.Email == email);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hash = SHA512.Create())
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