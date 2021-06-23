using System;
using SUS.MvcFramework;

namespace Musaca.Data.EntityModels
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Password { get; set; }
    }
}