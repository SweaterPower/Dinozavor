using System;
using System.Threading;

namespace Dinozavor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Google Dinosaur Jump game! Jump over everything!");
            Console.WriteLine("Press SPACE to jump.");
            Console.WriteLine("Press ANY KEY to start playing...");
            Console.ReadKey();
            bool isGameOver = false;
            Game.Init();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (!isGameOver)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        Game.dinosaur.Jump();
                    }
                }
            }));
            thread.Start();
            while (!isGameOver)
            {
                Game.GamePlay();
                isGameOver = Game.isCollided;
                Thread.Sleep(12);
            }
            Console.Clear();
            Console.WriteLine("YOU LOST!!!");
            Console.WriteLine("Your score is: {0}", Game.score);
            Console.WriteLine("Smash ANY KEY to exit the game...");
            Console.WriteLine("Rerun the game to try again.");
        }
    }
}
