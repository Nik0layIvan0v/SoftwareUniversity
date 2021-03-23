using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '\u25a0';
        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            InitializeWallBorders();
        }

        private void SetHorizontalLine(int topY)
        {
            for (int index = 0; index < this.LeftX; index++)
            {
                this.Draw(index, topY, WallSymbol);
            }
        }

        private void SetVerticalLine(int leftX)
        {
            for (int TopY = 0; TopY < this.TopY; TopY++)
            {
                this.Draw(leftX, TopY, WallSymbol);
            }
        }

        private void InitializeWallBorders()
        {
            this.SetHorizontalLine(0);
            this.SetHorizontalLine(this.TopY);
            
            this.SetVerticalLine(0);
            this.SetVerticalLine(this.LeftX - 1);
        }

        public bool IsPointOfWall(Point snake)
        {
            return snake.TopY == 0 || snake.LeftX == 0 || snake.LeftX == this.LeftX - 1 || snake.TopY == this.TopY;
        }
    }
}
