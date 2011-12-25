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

        //Bromsfunktion
        public float Acceleration(float speed, float friction, float force, int mass, float topspeed)
        {
            //Force är hur hårt vi bromsar. Kan vara mellan 0 och 1. Mass är bilens massa. Friction är friktionen mot underlaget.

            float Accel = friction / mass;
            if (force > 0)
            {
                if (speed < topspeed)
                    speed += (Accel / 60) * force; //Chansar på att vi har 60 i FPS.
                else
                    speed = topspeed;
            }
            else
                speed += (Accel / 60)*force;

            return speed;
        }
        public float Turn(float speed, float friction, int mass, Vector2 pos, Vector2 prevpos, float turn, int wheelbase)
        {
            //Turn är hur skarpt vi svänger, mellan -1 och 1. Pos är bilens nuvarande position och Prevpos är den förra positionen. Övriga variabler är samma som i Acceleration.
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
        /*
          Metoder att koda:
          drift(Vector2 speed, int radie, float friktion)
          TopSpeed(int HP, float friktion)
         */
    }
}
