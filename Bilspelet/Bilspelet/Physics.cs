using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bilspelet
{
    class Physics
    {

        //Accelerationsfuntion
        public float Acceleration(float speed, float friction, float force, int mass, float topspeed, Player playerCar)
        {
            //Force är hur hårt vi accelererar. Kan vara mellan -1 och 1. Mass är bilens massa. Friction är friktionen mot underlaget.
            playerCar.curfriction = (int)(playerCar.curfriction * Math.Sqrt(Math.Pow(force, 2)) * 0.2f);
            float Accel = friction / mass;
            if (force > 0)
            {
                if (speed < topspeed)
                    speed += (Accel / 60) * force; //Chansar på att vi har 60 i FPS.
                else
                    speed = topspeed;

            }
            else
                if (speed > -1 * (topspeed / 3))
                    speed += (Accel / 60) * force;
                else
                    speed = (topspeed / 3) * -1;
            
            return speed;
        }
        public float Turn(float speed, float friction, int mass, Vector2 pos, Vector2 prevpos, float turn, int wheelbase, Player playerCar)
        {
            //Turn är hur skarpt vi svänger, mellan -1 och 1. Pos är bilens nuvarande position och Prevpos är den förra positionen. Övriga variabler är samma som i Acceleration.
            playerCar.curfriction = (int)(playerCar.curfriction * Math.Sqrt(Math.Pow(turn, 2)) * -0.2f);
            float xaccel = (pos.X - prevpos.X) / (1f / 60f); //Utgår återigen från att FPS:en är 60.
            float yaccel = (pos.Y - prevpos.Y) / (1f / 60f);
            float force = (float)Math.Sqrt(Math.Pow(xaccel, 2) + Math.Pow(yaccel, 2));
            if (force > friction)
            {
                return turn * speed * 0.0005f * wheelbase*(friction/force);
            }
            else
            {
                return turn * speed * 0.0005f * wheelbase;
            }
        }
        public void Drift(Player playerCar, bool leftright)
        {
            //Drift är hur många grader bilen är roterad vi tillfället.
            //curfriction är hur mycket friktion som finns kvar efter våra manövrar den senaste uppdateringen, är denna under noll får vi sladd.
            if (playerCar.curfriction < 0)
            {
                if (playerCar.drift >= 1)
                {
                    playerCar.drift = 1;
                }
                else if (playerCar.drift <= -1)
                {
                    playerCar.drift = -1;
                }
                else
                {
                    if (leftright)
                    {
                        playerCar.drift += 0.05f;
                    }
                    else
                    {
                        playerCar.drift -= 0.05f;
                    }
                }
            }
            
        }//Pappa har sett och godkänt!!
    }
}
