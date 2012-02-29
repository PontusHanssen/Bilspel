using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Bilspelet
{
    public class AI : Car
    {
        public ArrayList copDest = new ArrayList();
        public float angle;
        int frame = 0;
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
        }
        public void Drive(Vector2 playerPos,int time)
        {
            if (frame % 15 == 0)
            {
                copDest.Add(playerPos);
            }
            if (copDest.Count > 0)
            {
                Vector2 V = (Vector2)copDest[0];
                double AdjacentCatethus = x - V.X;   //Anger avståndet  X led Närliggande katet i en triangel
                double OppositeCathetus = y - V.Y;  //anger avståndet  Y let Motstående
                angle = (float)Math.Atan(OppositeCathetus / AdjacentCatethus);           //räknar ut vinkeln mellan Närliggande och hypotenusa. Arctan(Mot/När)
                if (x >= V.X)
                    angle += MathHelper.Pi;
                if (new Rectangle((int)x-5, (int)y-5, texture.Width, texture.Height).Contains((int)V.X, (int)V.Y))
                {
                    copDest.RemoveAt(0);
                }
            }
            else
            {
                double AdjacentCatethus = x - playerPos.X;   //Anger avståndet  X led Närliggande katet i en triangel
                double OppositeCathetus = y - playerPos.Y;  //anger avståndet  Y let Motstående
                angle = (float)Math.Atan(OppositeCathetus / AdjacentCatethus);           //räknar ut vinkeln mellan Närliggande och hypotenusa. Arctan(Mot/När)
                if (x >= playerPos.X)
                    angle += MathHelper.Pi;
            }
            x += (float)(Math.Cos(angle) * 9.3);
            y += (float)(Math.Sin(angle)*9.3);
            frame++;
        }
    }
}