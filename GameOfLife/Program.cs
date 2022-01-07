using System;
using System.Drawing;

namespace GameOfLife
{
    public static class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            int width = Console.WindowWidth - 2;
            int height = (Console.WindowHeight * 2) - 2;

            var consoleDrawer = new ConsoleDrawer(new Point(0, 0), width, height);
            var gameController = new GameController(width, height);
            gameController.Draw += consoleDrawer.Draw;
            gameController.Play();
            gameController.Draw -= consoleDrawer.Draw;
            Console.ReadLine();
        }
    }
}