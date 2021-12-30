using System;
using System.Collections.Generic;
using System.Text;

namespace Dinozavor
{
    static class Game
    {
        public static Dinosaur dinosaur;
        static int[,] screen;
        static Random rnd;
        static public bool isCollided = false;
        static int obstacleTimeout = 20;
        static int obstacleCounter = 0;
        static public int groundLevel = 1;
        static public int score = 0;

        static public void Init()
        {
            dinosaur = new Dinosaur();
            dinosaur.position = new Point(30, groundLevel);
            rnd = new Random();
            screen = new int[20, 80];
            for (int j = 0; j < screen.GetLength(1); j++)
                screen[screen.GetLength(0) - 1, j] = 1;
            ShiftField();
        }

        static public void GamePlay()
        {
            ShiftField();
            CheckCollision();
            GenerateObstacle();
            dinosaur.GameTick();
            DrawField();
        }

        static void ShiftField()
        {
            for (int i = 0; i < screen.GetLength(0); i++)
                for (int j = 0; j < screen.GetLength(1) - 1; j++)
                    screen[i, j] = screen[i, j + 1];
            for (int j = 0; j < screen.GetLength(0) - 1; j++)
                screen[j, screen.GetLength(1) - 1] = 0;
            screen[screen.GetLength(0) - 1, screen.GetLength(1) - 1] = 1;
        }

        static void GenerateObstacle()
        {
            if (obstacleCounter == obstacleTimeout)
            {
                score += 100;
                obstacleCounter = 0;
                int height = rnd.Next(1, score > 2000 ? 5 : 4);
                int xMax = screen.GetLength(1) - 1;
                int yMax = screen.GetLength(0) - 2;
                for (int i = 0; i < height; i++)
                    screen[yMax - i, xMax] = 1;
            }
            else
                obstacleCounter++;
        }

        static void CheckCollision()
        {
            int y = screen.GetLength(0) - 1 - dinosaur.position.Y;
            if (dinosaur.isJumping)
                y -= 1;
            for (int i = 0; i < dinosaur.width; i++)
            {
                if (screen[y, dinosaur.position.X + i] == 1)
                {
                    isCollided = true;
                    Console.WriteLine("!!! PRESS ANY KEY !!!");
                }
            }
        }

        static void DrawField()
        {
            Console.Clear();
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    if (screen[i, j] == 1)
                        Console.Write("0");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(dinosaur.position.X, screen.GetLength(0) - 1 - dinosaur.position.Y);
            if (dinosaur.isJumping)
            {
                Console.Write(dinosaur.nogi1);
            }
            else
            {
                Console.Write(dinosaur.Step());
            }
            for (int i = 0; i < dinosaur.body.Length; i++)
            {
                Console.SetCursorPosition(dinosaur.position.X, screen.GetLength(0) - 1 - dinosaur.position.Y - dinosaur.height + 1 + i);
                Console.Write(dinosaur.body[i]);
            }
            Console.SetCursorPosition(25, 2);
            Console.Write("Score: {0}", score);
        }
    }
}
