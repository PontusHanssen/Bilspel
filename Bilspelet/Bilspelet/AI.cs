using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Bilspelet
{
   public class AI : Car
    {
       public Vector2[] Targets = new Vector2[15];
       public float angle;
        public AI(int x, int y, int mass, int speed, int friction, Texture2D texture, bool onroad)
        {
            this.x = x;
            this.y = y;
            this.mass = mass;
            this.speed = speed;
            this.friction = friction;
            this.texture = texture;
            //this.driftsound = driftsound;
            this.onRoad = onroad;
            this.speedX = 0;
            this.speedY = 0;
            this.wheelbase = 28;
            this.acceleration = 0;
            RestartLap();
        }
        public void Drive(Game1 G)
        {
            //Targets[14] = G.playerCar.position;
            float minDist = 0,VectNr=0;
            for (int i = 0; i < 13; i++)
            {
                Targets[i] += G.map.Distance;
            }
            for(int i =0;i<Targets.Length;i++)
            {
                Vector2 currVect;
                if (i == Targets.Length)
                {
                    currVect = G.playerCar.position;
                }
                else
                {
                     currVect = Targets[i];
                } 
                double AdjacentCathetus = Math.Pow((currVect.X  - G.playerCar.position.X), 2);
                double OppositeCathetus = Math.Pow((currVect.Y - G.playerCar.position.Y), 2);
                double Hypothenouse = AdjacentCathetus + OppositeCathetus;
                if (Hypothenouse < 100)
                {

                    Hypothenouse = double.MaxValue;
                    currVect = new Vector2(float.MaxValue, float.MaxValue);
                    Targets[i] = currVect;
                }
               
                if (Hypothenouse < minDist|| minDist == 0)
                {
                    minDist = (float)Hypothenouse;
                    VectNr = i;
                }
            }
            Vector2 Target = Targets[(int)VectNr];
            double AdCatethus = x - Target.X;   //Anger avståndet  X led Närliggande katet i en triangel
            double OpCathetus = y - Target.Y;  //anger avståndet  Y let Motstående
            angle = (float)(Math.Atan(OpCathetus / AdCatethus));           //räknar ut vinkeln mellan Närliggande och hypotenusa. Arctan(Mot/När)
            if (x >= Target.X)
                angle += (float)Math.PI;
            x += speed * (float)Math.Cos(angle);
            y += speed * (float)Math.Sin(angle);
            if (VectNr == 0)
                RestartLap();
        }
        public void RestartLap()
        {
            Targets[0] = new Vector2(1377, 379);
            Targets[1] = new Vector2(2674, 533);
            Targets[2] = new Vector2(3486, 187);
            Targets[3] = new Vector2(3757, 327);
            Targets[4] = new Vector2(3279, 675);
            Targets[5] = new Vector2(3439, 901);
            Targets[6] = new Vector2(2595, 767);
            Targets[7] = new Vector2(1917, 1356);
            Targets[8] = new Vector2(1310, 832);
            Targets[9] = new Vector2(960, 960);
            Targets[10] = new Vector2(748, 1358);
            Targets[11] = new Vector2(428, 1274);
            Targets[12] = new Vector2(84, 966);
            Targets[13] = new Vector2(60, 654);
        }
    }
}
