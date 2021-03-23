using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private readonly Wall wall;
        private readonly Random randomPosition;
        private readonly char foodSymbol;

        protected Food(Wall wall, char foodSymbol, int foodPoints) 
            : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            this.randomPosition = new Random();
            this.FoodPoints = foodPoints;
        }
        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snakePartsPosition)
        {
            this.LeftX = randomPosition.Next(2, wall.LeftX - 2);
            this.TopY = randomPosition.Next(2, wall.TopY - 2);

            bool isPointOfSnake = snakePartsPosition.Any(x => x.LeftX == this.LeftX && x.TopY == this.TopY);

            while (isPointOfSnake)
            {
                this.LeftX = randomPosition.Next(2, wall.LeftX - 2);
                this.TopY = randomPosition.Next(2, wall.TopY - 2);

                isPointOfSnake = snakePartsPosition.Any(x => x.LeftX == this.LeftX && x.TopY == this.TopY);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            this.Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snakePoint)
        {
            return snakePoint.TopY == this.TopY && snakePoint.LeftX == this.LeftX;
        }
    }
}
