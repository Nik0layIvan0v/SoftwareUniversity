using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSnake.GameObjects.FoodTypes;

namespace SimpleSnake.GameObjects.SnakeObject
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';
        private Queue<Point> snakeParts;
        private Food[] food;
        private Wall wall;
        private int foodIndex;
        public Snake(Wall wall)
        {

            this.wall = wall;
            this.snakeParts = new Queue<Point>();
            this.food = new Food[3];
            this.foodIndex = RandomFoodNumber;
            this.GetFoods();
            this.CreateSnake();
        }

        public int NextLeftX { get; private set; }

        public int NextTopY { get; private set; }

        private int RandomFoodNumber => new Random().Next(0, this.food.Length);
        private void CreateSnake()
        {
            for (int TopY = 1; TopY <= 6; TopY++)
            {
                this.snakeParts.Enqueue(new Point(2, TopY));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeParts);
        }

        private void GetFoods()
        {
            this.food[0] = new FoodHash(this.wall);
            this.food[1] = new FoodDollar(this.wall);
            this.food[2] = new FoodAsterisk(this.wall);
        }

        public bool IsMoving(Point directionPoint)
        {
            Point currentSnakeHead = this.snakeParts.Last();

            GetNextPoint(directionPoint, currentSnakeHead);

            bool isPointOfSnake = this.snakeParts.Any(x => x.LeftX == this.NextLeftX && x.TopY == this.NextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point snakeNewHead = new Point(this.NextLeftX, this.NextTopY);

            if (this.wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.snakeParts.Enqueue(snakeNewHead);

            snakeNewHead.Draw(snakeSymbol);

            if (food[foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(directionPoint, currentSnakeHead);
            }

            Point snakeTail = this.snakeParts.Dequeue();

            snakeTail.Draw(' ');

            return true;
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.NextLeftX = snakeHead.LeftX + direction.LeftX;
            this.NextTopY = snakeHead.TopY + direction.TopY;
        }

        private void Eat(Point direction, Point currSnakeHead)
        {
            int length = food[foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snakeParts.Enqueue(new Point(this.NextLeftX, this.NextTopY));

                GetNextPoint(direction, currSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeParts);
        }

    }
}
