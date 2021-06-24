using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BattleCards.Data.DataConstants;

namespace BattleCards.Data.EntityModels
{
    public class Card
    {
        public Card()
        {
            this.UserCards = new HashSet<UserCard>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCardNameLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        //cannot be negative
        public int Attack { get; set; }

        //cannot be negative
        public int Health { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public ICollection<UserCard> UserCards { get; set; }
    }
}