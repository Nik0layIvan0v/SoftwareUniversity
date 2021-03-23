using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using SimpleSnake.GameObjects.SnakeObject;
using System;
using System.Threading;

namespace SimpleSnake.Core
{
    public class Engine : IEngine
    {
        private readonly Point[] pointsOfDirection;
        private readonly Wall wall;
        private readonly Food food;
        private double sleepTime;
        private readonly Snake snake;
        private Direction direction = Direction.Right;

        public Engine(Wall wall, Snake snake)
        {
            this.wall = wall;
            this.snake = snake;
            this.sleepTime = 100;
            this.pointsOfDirection = new Point[4];
        }

        private void CreateDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {

            ConsoleKeyInfo userInput = Console.ReadKey();


            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
            }
            if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
            }

            bool isMoving = snake.IsMoving(this.pointsOfDirection[(int)direction]);

            if (!isMoving)
            {
                AskUserForRestart();
            }

            Console.CursorVisible = false;
        }

        private void AskUserForRestart()
        {
            int leftX = this.wall.LeftX + 1;
            int topY = 3;

            Console.SetCursorPosition(leftX, topY);

            Console.Write("Would you like to continue? y/n");

            string intput = Console.ReadLine();

            if (intput == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                StopGame();
            }
        }

        public void Run()
        {
            this.CreateDirections();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = snake.IsMoving(this.pointsOfDirection[(int)direction]);

                if (!isMoving)
                {
                    AskUserForRestart();
                }

                sleepTime -= 0.001;

                Thread.Sleep((int)sleepTime);
            }
        }

        private void StopGame()
        {
            Console.SetCursorPosition(20, 10);
            Console.Write("Game Over!");
            Environment.Exit(0);
        }
    }
}
