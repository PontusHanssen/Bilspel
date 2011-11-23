using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilspelet
{
    class Physics
    {

        //Bromsfunktion
        public float Brake(float speed, float friction, float force, int mass)
        {
            //Force är hur hårt vi bromsar. Kan vara mellan 0 och 1. Mass är bilens massa. Friction är friktionen mot underlaget.

            float Accel = friction / mass;  
            speed -= (Accel / 60) * force; //Chansar på att vi har 60 i FPS.
            //speed -= 0.04f;
            return speed;
        }
        /*
          Metoder att koda:
          drift(Vector2 speed, int radie, float friktion)
          TopSpeed(int HP, float friktion)
         */
    }
}
