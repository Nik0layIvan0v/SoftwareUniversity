namespace BattleCards.ViewModels.CardsViewModels
{
    public class HomePageCardViewModel
    {
        public int Id { get; set; }

        public string CardName { get; set; }

        public string CardImage { get; set; }

        public string Description { get; set; }

        public string Keyword { get; set; }

        public int HealthPoints{ get; set; }

        public int AttackPoints { get; set; }
    }
}