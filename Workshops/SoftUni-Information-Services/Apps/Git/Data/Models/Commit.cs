using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Git.Data.Models
{
    public class Commit
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        //string with min length 5 (required)
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        //[ForeignKey(nameof(User))]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        [Required]
        //[ForeignKey("Repository")]
        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}