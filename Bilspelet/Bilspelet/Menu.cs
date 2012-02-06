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
    public class Menu
    {
        private Texture2D _background;
        private Texture2D _menuitem;
        private Texture2D _selectedmenuitem;
        private int _width;
        private int _height;

        public Texture2D background
        {
            get { return _background; }
            set { _background = value; }
        }
        public Texture2D menuitem
        {
            get { return _menuitem; }
            set { _menuitem = value; }
        }
        public Texture2D selectedmenuitem
        {
            get { return _selectedmenuitem; }
            set { _selectedmenuitem = value; }
        }
        public Rectangle bgrectangle
        {
            get{return new Rectangle(0,0,_width,_width);}
            set{_width = value.Width; _height = value.Height;}
        }
        public Menu(Game1 g)
        {
            _background = g.Content.Load<Texture2D>(@"Textures/menubg");
            _width = g.GraphicsDevice.DisplayMode.Width;
            _height = g.GraphicsDevice.DisplayMode.Height;
        }
        


    }
}
