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
    public class Player : Car
    {
        private int _hp;

        public int hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public Player(int x, int y, int hp, int mass, int speed, int friction, Texture2D texture, bool onroad)
        {
            this.x = x;
            this.y = y;
            this.hp = hp;
            this.mass = mass;
            this.speed = speed;
            this.friction = friction;
            this.curfriction = friction;
            this.drift = 0f;
            this.texture = texture;
            //this.driftsound = driftsound;
            this.onRoad = onroad;
            this.speedX = 0;
            this.speedY = 0;
            this.wheelbase = 28;
            this.acceleration = 0;
        }
    }
}
