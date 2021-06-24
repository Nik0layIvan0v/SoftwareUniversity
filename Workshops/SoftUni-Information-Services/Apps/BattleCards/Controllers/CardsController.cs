using System;
using System.Collections.Generic;
using System.Linq;
using BattleCards.Models.Cards;
using BattleCards.Services;
using BattleCards.ViewModels.CardsViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using static BattleCards.Data.DataConstants;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private protected readonly CardsService CardService;

        public CardsController(CardsService cardService)
        {
            this.CardService = cardService;
        }

        //GET
        public HttpResponse All()
        {
            //All created Cards are visualized on the Home Page

            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            List<HomePageCardViewModel> cardsViewModels = this.CardService.GetAllCards();

            return this.View(cardsViewModels);
        }

        //GET
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel cardFormInput)
        {
            //!!!When User adds a Card, it has to be added to their collection too!!!

            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            if (string.IsNullOrWhiteSpace(cardFormInput.Image) ||
                string.IsNullOrWhiteSpace(cardFormInput.Name) ||
                string.IsNullOrWhiteSpace(cardFormInput.Keyword) ||
                string.IsNullOrWhiteSpace(cardFormInput.Description))
            {
                return this.Error("All fields are required!");
            }

            if (cardFormInput.Name.Length < MinCardNameLength ||
                cardFormInput.Name.Length > MaxCardNameLength)
            {
                return this.Error(
                    $"Card name: {cardFormInput.Name} is invalid. Recommended min length is {MinCardNameLength} and max length is {MaxCardNameLength}!");
            }

            if (Uri.IsWellFormedUriString(cardFormInput.Image, UriKind.RelativeOrAbsolute) == false)
            {
                return this.Error("Invalid image URL!");
            }

            if (cardFormInput.Attack < 0 || cardFormInput.Health < 0)
            {
                return this.Error("Invalid Health or Attack. These fields cannot contains negative numbers!");
            }

            if (AllowedKeywords.All(kw => kw.ToLower() != cardFormInput.Keyword.ToLower()))
            {
                return this.Error($"Invalid Keyword! Allowed keywords are {string.Join(", ", AllowedKeywords)}.");
            }

            if (cardFormInput.Description.Length > MaxDescriptionLength)
            {
                return this.Error($"Invalid Description! Max length is {MaxDescriptionLength}");
            }

            int cardId = this.CardService.AddCard(cardFormInput);

            string userId = this.GetUserId();

            this.CardService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        //GET
        public HttpResponse Collection()
        {
            //Users have a Collection page where only the Cards in their collection are visualized.

            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }
            string userId = this.GetUserId();

            var myCardCollection = this.CardService.GetMyCardCollection(userId);

            return this.View(myCardCollection);
        }

        //GET
        public HttpResponse AddToCollection(int cardId)
        {
            //•	If a User tries to add an already contained Card to their collection, they should be redirected to /Cards/All (or just a page refresh).
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            string userId = this.GetUserId();

            if (this.CardService.IsUserCollectionContainsCard(userId, cardId))
            {
                return this.Error("You already contains this card!");
            }

            this.CardService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        //GET
        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            string userId = this.GetUserId();

            if (this.CardService.IsUserCollectionContainsCard(userId, cardId) == false)
            {
                return this.Error("You don't own this card!");
            }

            this.CardService.RemoveCardFromUserCollection(userId, cardId);

            return this.Redirect("/Cards/Collection");
        }
    }
}