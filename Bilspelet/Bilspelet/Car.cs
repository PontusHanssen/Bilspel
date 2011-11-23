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
        private int HP;
        private float angle;
        private bool onroad;
        private int x;
        private int y;
        private float speedx;
        private float speedy;
        private SoundEffect driftsound;
        private SoundEffect enginesound;
        private Texture2D texture;

        public float Wheels{
            get{return wheels;}
            set{
    }
}
