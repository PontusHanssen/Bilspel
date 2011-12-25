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
    public class Car
    {
        private float _wheels;
        private float _angle;
        private float _speedx;
        private float _speedy;
        private float _speed;
        private int _friction;
        private int _mass;
        private int _hp;
        private int _wheelbase;
        private float _x;
        private float _y;
        private float _prevx;
        private float _prevy;
        private float _acceleration;
        private Vector2 _position;
        private Vector2 _prevpos;
        private bool _onroad;
        private SoundEffect _driftsound;
        private Texture2D _texture;

        public float wheels
        {
            get { return _wheels; }
            set { _wheels = value; }
        }
        public float angle
        {
            get { return _angle; }
            set { _angle = value; }
        }
        public float speedX
        {
            get { return _speedx; }
            set { _speedx = value; }
        }
        public float speedY
        {
            get { return _speedy; }
            set { _speedy = value; }
        }
        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }
        public float topspeed
        {
            get { return mass * friction / 50; }
        }
            public int hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public int wheelbase
        {
            get { return _wheelbase; }
            set { _wheelbase = value; }
        }
        public float x
        {
            get { return _x; }
            set { _x = value; }
        }
        public float y
        {
            get { return _y; }
            set { _y = value; }
        }
        public float prevx
        {
            get { return _prevx; }
            set { _prevx = value; }
        }
        public float prevy
        {
            get { return _prevy; }
            set { _prevy = value; }
        }
        public int friction
        {
            get { return _friction; }
            set { _friction = value; }
        }
        public int mass
        {
            get { return _mass; }
            set { _mass = value; }
        }
        public bool onRoad
        {
            get { return _onroad; }
            set { _onroad = value; }
        }
        public Rectangle rectangle
        {
            get { return new Rectangle((int)_x, (int)_y, _texture.Width, _texture.Height); }
        }
        public Vector2 position
        {
            get { return new Vector2(_x,_y); }
            set
            {
                _x = _position.X;
                _y = _position.Y;
            }

        }
        public Vector2 prevpos
        {
            get { return new Vector2(_prevx, _prevy); }
            set
            {
                _prevx = _prevpos.X;
                _prevy = _prevpos.Y;
            }

        }

        public Texture2D texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public SoundEffect driftSound
        {
            get { return _driftsound; }
            set { _driftsound = value; }
        }


            


    }
}
