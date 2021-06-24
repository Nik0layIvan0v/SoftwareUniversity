using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BattleCards.Data.DataConstants;

namespace BattleCards.Data.EntityModels
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCards = new HashSet<UserCard>();
        }

        [Key]
        [Required]
        public string Id { get; set; } 

        [Required]
        [MaxLength(MaxUsernameLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserCard> UserCards { get; set; }

    }
}
