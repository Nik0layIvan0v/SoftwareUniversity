using System.Collections.Generic;
using BattleCards.Models.Cards;
using BattleCards.ViewModels.CardsViewModels;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        int AddCard(AddCardInputModel inputModel);

        List<HomePageCardViewModel> GetAllCards();

        void AddCardToUserCollection(string userId, int cardId);

        void RemoveCardFromUserCollection(string userId, int cardId);

        bool IsUserCollectionContainsCard(string userId, int cardId);
    }
}