namespace SimpleSnake.GameObjects.FoodTypes
{
    public class FoodAsterisk : Food
    {
        private const char foodSymbol = '*';

        private const int foodPoints = 1;
        public FoodAsterisk(Wall wall) 
            : base(wall, foodSymbol, foodPoints)
        {
        }
    }
}
