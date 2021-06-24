using System.Collections.Generic;
using System.Linq;
using BattleCards.Data;
using BattleCards.Data.EntityModels;
using BattleCards.Models.Cards;
using BattleCards.ViewModels.CardsViewModels;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private protected readonly ApplicationDbContext DbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int AddCard(AddCardInputModel inputModel)
        {
            Card card = new Card
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.Image,
                Keyword = inputModel.Keyword,
                Attack = inputModel.Attack,
                Health = inputModel.Health,
                Description = inputModel.Description,
            };

            this.DbContext.Cards.Add(card);
            this.DbContext.SaveChanges();

            return card.Id;
        }

        public List<HomePageCardViewModel> GetAllCards()
        {
            return this.DbContext
                .Cards
                .Select(card => new HomePageCardViewModel
                {
                    Id = card.Id,
                    CardName = card.Name,
                    CardImage = card.ImageUrl,
                    Description = card.Description,
                    Keyword = card.Keyword,
                    HealthPoints = card.Health,
                    AttackPoints = card.Attack
                })
                .ToList();
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            UserCard userCard = new UserCard
            {
                UserId = userId,
                CardId = cardId,
            };

            this.DbContext.UserCards.Add(userCard);
            this.DbContext.SaveChanges();
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            UserCard userCard = new UserCard
            {
                UserId = userId,
                CardId = cardId,
            };

            this.DbContext.UserCards.Remove(userCard);
            this.DbContext.SaveChanges();
        }

        public bool IsUserCollectionContainsCard(string userId, int cardId)
        {
            return this.DbContext.UserCards.Any(x => x.CardId == cardId && x.UserId == userId);
        }

        public List<HomePageCardViewModel> GetMyCardCollection(string userId)
        {
            return this.DbContext
                .Cards
                .Where(card => card.UserCards.Any(userCard => userCard.UserId == userId))
                .Select(card => new HomePageCardViewModel
                {
                    Id = card.Id,
                    CardName = card.Name,
                    CardImage = card.ImageUrl,
                    Description = card.Description,
                    Keyword = card.Keyword,
                    HealthPoints = card.Health,
                    AttackPoints = card.Attack
                })
                .ToList();
        }
    }
}