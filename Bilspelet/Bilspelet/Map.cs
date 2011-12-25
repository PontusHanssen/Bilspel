using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bilspelet
{
    class Map
    {
        private Texture2D _map;
        private Vector2 _picPos = new Vector2();
        public Vector2 _goal = new Vector2(1839,218), _checkpoint = new Vector2(3234,640);
        private bool _hasPassed = false;

        public Map(Game1 g)
        { map = g.Content.Load<Texture2D>(@"./Textures/map"); }

        public Texture2D map
        {
            get { return _map; }
            set { _map = value; }
        }

        public Vector2 picPos
        {
            get { return _picPos; }
            set { _picPos = value; }
        }

        public void SetCamera(Game1 g)
        {
            Vector2 Distance = new Vector2(1280, 800) / 2 - new Vector2(g.playerCar.x, g.playerCar.y);
            g.playerCar.x += Distance.X;
            g.playerCar.y += Distance.Y;
            g.copCar.x += Distance.X;
            g.copCar.y += Distance.Y;
            picPos += Distance;
            _goal.X += Distance.X;
            _goal.Y += Distance.Y;
            _checkpoint.X += Distance.X;
            _checkpoint.Y += Distance.Y;

        }
        public void CheckCheckpoints(Game1 g)
        {
            if (_hasPassed)
                if (new Rectangle((int)g.playerCar.x, (int)g.playerCar.y, 1, 1).Intersects(new Rectangle((int)_goal.X, (int)_goal.Y, 100, 500)))
                {
                    g.laps++;
                    _hasPassed = !_hasPassed;
                }
                else ;
            else if (new Rectangle((int)g.playerCar.x, (int)g.playerCar.y, 1, 1).Intersects(new Rectangle((int)_checkpoint.X, (int)_checkpoint.Y, 100, 500)))
                _hasPassed = true;
        }
    }
}
