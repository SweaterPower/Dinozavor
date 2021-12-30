using System;
using System.Collections.Generic;
using System.Text;

namespace Dinozavor
{
    class Dinosaur
    {
        //visual templates
        public string[] body = new string[] {"  ####", " ###  ", " #####", " ##   ", "####  ", "######", "##### "};
        public string nogi1 = "#   # ";
        string nogi2 = " # #  ";
        int stepCount = 1;

        //dino size
        public int height = 8;
        public int width = 6;
       
        //jump data
        int flightDuration = 2;
        int currentFlight = 0;
        int jumpHeight = 5;
        public bool isJumping = false;
        bool isJumpEnded = true;
        bool isFlightEnded = false;

        //position on screen and on game field
        public Point position;

        public void Jump()
        {
            isJumping = true;
            isJumpEnded = false;
            isFlightEnded = false;
            currentFlight = 0;
        }

        public void GameTick()
        {
            if (isJumping)
            {
                if (isJumpEnded)
                {
                    if (isFlightEnded)
                    {
                        if (position.Y > Game.groundLevel)
                            position.Y--;
                        else
                            isJumping = false;
                    }
                    else
                    {
                        if (currentFlight < flightDuration)
                            currentFlight++;
                        else
                            isFlightEnded = true;
                    }
                }
                else
                {
                    if (position.Y < jumpHeight)
                        position.Y++;
                    else
                        isJumpEnded = true;
                }
            }
        }

        public string Step()
        {
            if (stepCount == 1)
            {
                stepCount = 2;
                return nogi1;
            }
            else
            {
                stepCount = 1;
                return nogi2;
            }
        }
    }
}
