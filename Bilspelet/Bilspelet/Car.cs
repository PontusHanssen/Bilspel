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
    class Car
    {
        private float wheels;
        private float angle;
        private float speedx;
        private float speedy;
        private int hp;
        private int x;
        private int y;
        private bool onroad;
        private SoundEffect driftsound;
        private Texture2D texture;

        public float Wheels
        {
            get { return wheels; }
            set { wheels = value; }
        }
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public float SpeedX
        {
            get { return speedx; }
            set { speedx = value; }
        }
        public float SpeedY
        {
            get { return speedy; }
            set { speedy = value; }
        }
        public int HP
        {
            get { return hp; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public bool OnRoad
        {
            get { return onroad; }
            set { onroad = value; }
        }
        public Rectangle Collision
        {
            get { return new Rectangle(x, y, texture.Width, texture.Height); }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public SoundEffect DriftSound
        {
            get { return driftsound; }
            set { driftsound = value; }
        }
        public Car(int x, int y, int hp, Texture2D texture, SoundEffect driftsound, bool onroad)
        {
            this.x = x;
            this.y = y;
            this.hp = hp;
            this.texture = texture;
            this.driftsound = driftsound;
            this.onroad = onroad;
            this.speedx = 0;
            this.speedy = 0;
        }

            


    }
}
