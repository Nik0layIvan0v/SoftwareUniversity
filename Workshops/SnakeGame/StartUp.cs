using SimpleSnake.Core;
using SimpleSnake.GameObjects;
using SimpleSnake.GameObjects.SnakeObject;

namespace SimpleSnake
{
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Wall wall = new Wall(50, 50);

            Snake snake = new Snake(wall);

            Engine engine = new Engine(wall, snake);

            engine.Run();
        }
    }
}
