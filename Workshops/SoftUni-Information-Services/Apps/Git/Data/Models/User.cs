using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        //a string with min length 5 and max length 20 (required)
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        //a string with min length 6 and max length 20  - hashed in the database (required)
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}