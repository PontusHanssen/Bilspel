using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bilspelet
{
    public class Map
    {
        private Texture2D _map;
        public Texture2D _map2;
        private Vector2 _picPos = new Vector2();
        private Vector2 _picPos2 = new Vector2();
        public Vector2 _goal = new Vector2(1439,218), _checkpoint = new Vector2(3234,640);
        public Vector2 Distance;
        private bool _hasPassed = false;

        public Map(Game1 g)
        { map = g.Content.Load<Texture2D>(@"./Textures/map");
        map2 = g.Content.Load<Texture2D>(@"./Textures/mapcheck");
        }

        public Texture2D map
        {
            get { return _map; }
            set { _map = value; }
        }
        public Texture2D map2
        {
            get { return _map2; }
            set { _map2 = value; }
        }

        public Vector2 picPos
        {
            get { return _picPos; }
            set { _picPos = value; }
        }
        public Vector2 picPos2
        {
            get { return _picPos2; }
            set { _picPos2 = value; }
        }

        public void SetCamera(Game1 g, int X, int Y)
        {
            Distance = new Vector2(X, Y) / 2 - new Vector2(g.playerCar.x, g.playerCar.y);
            if (map.Bounds.Right + _picPos.X < X && Distance.X < 0 || _picPos.X > 0 && Distance.X > 0) ;
            else
            {
                g.playerCar.x += Distance.X;
                g.copCar.x += Distance.X;
                _picPos.X += Distance.X;
                _picPos2.X += Distance.X;
                _goal.X += Distance.X;
                _checkpoint.X += Distance.X;
            }
            if (map.Bounds.Bottom + _picPos.Y < Y && Distance.Y < 0 || _picPos.Y > 0 && Distance.Y > 0) ;
            else
            {
                g.playerCar.y += Distance.Y;
                g.copCar.y += Distance.Y;
                _goal.Y += Distance.Y;
                _checkpoint.Y += Distance.Y;
                _picPos.Y += Distance.Y;
                _picPos2.Y += Distance.Y;
            }
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
        public bool OnRoad(Game1 G)
        {
            Color[] RetrievedColor = new Color[4];
            
            map2.GetData<Color>(0, new Rectangle((int)(G.playerCar.x-picPos2.X + Math.Cos(G.playerCar.angle + G.playerCar.drift) * 50), (int)(G.playerCar.y-picPos2.Y + Math.Sin(G.playerCar.angle + G.playerCar.drift) * 50), 1, 1), RetrievedColor, 0, 1);
            map2.GetData<Color>(0, new Rectangle((int)(G.playerCar.x - picPos2.X + Math.Cos(G.playerCar.angle + G.playerCar.drift) * 0), (int)(G.playerCar.y- picPos2.Y + Math.Sin(G.playerCar.angle + G.playerCar.drift) * 0), 1, 1), RetrievedColor, 1, 1);
            map2.GetData<Color>(0, new Rectangle((int)(G.playerCar.x - picPos2.X + Math.Cos(G.playerCar.angle + G.playerCar.drift + 0.525) * 57.8), (int)(G.playerCar.y - picPos2.Y + Math.Sin(G.playerCar.angle + 0.525 + G.playerCar.drift) * 57.8), 1, 1), RetrievedColor, 2, 1);
            map2.GetData<Color>(0, new Rectangle((int)(G.playerCar.x - picPos2.X + Math.Cos(G.playerCar.angle + G.playerCar.drift + MathHelper.PiOver2) * 29), (int)(G.playerCar.y - picPos2.Y + Math.Sin(G.playerCar.angle + G.playerCar.drift + MathHelper.PiOver2) * 29), 1, 1), RetrievedColor, 3, 1);

            for (int i = 0; i < RetrievedColor.Length; i++)
                if (RetrievedColor[i].B >125)
                {
                    return false;
                }
                else
                    if (i == 3)
                        return true;
            return true;
        }
    }
}
