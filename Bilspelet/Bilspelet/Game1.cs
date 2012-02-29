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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont Font;
        public Map map;
        public AI copCar;
        public Player playerCar;
        Physics Physics = new Physics();
        Song BackgroundMusic;
        public Menu menu;
        public SoundEffect checkpointSound;
        public int laps = 0;
        bool leftright;
        int gamestate = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BackgroundMusic = Content.Load<Song>(@"Sounds/ontheroadagain");
            //   checkpointSound = Content.Load<SoundEffect>(@"Sounds/checkpoint");
            Font = Content.Load<SpriteFont>(@"Fonts/Font");
            copCar = new AI(1200, 300, 10, 5, 10, Content.Load<Texture2D>(@"Textures/red"), true);
            playerCar = new Player(1300, 350, 10, 5, 0, 100, Content.Load<Texture2D>(@"Textures/blue"), true);
            map = new Map(this);
            menu = new Menu(this);
            //PlayerCar = new Car(100, 100, 10, 1000, 5, 2000, Content.Load<Texture2D>("./Textures/blue"), true);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            playerCar.curfriction = playerCar.friction;

            GamePadState GPad1 = GamePad.GetState(PlayerIndex.One);
            //gamestate 0 är meny, 1 är racemeny, 2 är fritt spel, 3 är trim
            if (gamestate == 0)
            {
                if (GPad1.Buttons.A == ButtonState.Pressed)
                {
                    gamestate = 1;
                    //MediaPlayer.Play(BackgroundMusic);

                }
            }
            if (gamestate == 1)
            {
                playerCar.curfriction = playerCar.friction;
                //Kör i cirklar.
                //PlayerCar.Angle += turn;
                map.SetCamera(this, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                map.CheckCheckpoints(this);


                playerCar.prevx = playerCar.x;
                playerCar.prevy = playerCar.y;
                if (GPad1.Triggers.Left > 0)
                {
                    playerCar.acceleration = GPad1.Triggers.Left * -1;
                }
                else
                {
                    if (GPad1.Triggers.Right == 0)
                    {
                        if (playerCar.speed > 0)
                        {
                            playerCar.acceleration = -0.5f;
                        }
                        else
                        {
                            playerCar.acceleration = 0;
                        }
                    }
                    else
                    {
                        playerCar.acceleration = GPad1.Triggers.Right;
                    }
                }

                if (playerCar.onRoad)
                {
                    playerCar.speed = Physics.Acceleration(playerCar.speed, playerCar.friction, playerCar.acceleration, playerCar.mass, playerCar.topspeed, playerCar);
                }
                else
                {
                    if (playerCar.speed > 1.0f)
                    {
                        playerCar.speed = Physics.Acceleration(playerCar.speed, playerCar.friction, -0.5f, playerCar.mass, playerCar.topspeed, playerCar);
                    }
                    else
                    {
                        playerCar.speed = Physics.Acceleration(playerCar.speed, playerCar.friction, playerCar.acceleration * 0.1f, playerCar.mass, playerCar.topspeed, playerCar);
                    }
                }
                playerCar.x += (float)Math.Cos(playerCar.angle) * playerCar.speed;
                playerCar.y += (float)Math.Sin(playerCar.angle) * playerCar.speed;

                playerCar.angle += Physics.Turn(playerCar.speed, playerCar.friction, playerCar.mass, playerCar.position, playerCar.prevpos, GPad1.ThumbSticks.Left.X, playerCar.wheelbase, playerCar);

                Physics.Drift(playerCar, leftright);
                if (GPad1.ThumbSticks.Left.X > 0)
                    leftright = true;
                else
                {
                    leftright = false;
                }

                if (playerCar.drift < 0 && GPad1.ThumbSticks.Left.X <= 0)
                    playerCar.drift += 0.04f;
                else if (playerCar.drift > 0 && GPad1.ThumbSticks.Left.X >= 0)
                    playerCar.drift -= 0.04f;

                if ((playerCar.x - map.picPos2.X + Math.Cos(playerCar.angle + playerCar.drift) * 50) < 1)
                {
                    playerCar.x = 10;
                }
                if ((playerCar.x - map.picPos2.X + Math.Cos(playerCar.angle + playerCar.drift) * 0) < 1)
                {
                    playerCar.x = 10;
                }
                if ((playerCar.x - map.picPos2.X + Math.Cos(playerCar.angle + playerCar.drift + 0.525) * 57.8) < 1)
                {
                    playerCar.x = 10;
                }
                if ((playerCar.x - map.picPos2.X + Math.Cos(playerCar.angle + playerCar.drift + MathHelper.PiOver2) * 29) < 1)
                {
                    playerCar.x = 10;
                }

                if (playerCar.position.X > graphics.PreferredBackBufferWidth)
                {
                    playerCar.x = graphics.PreferredBackBufferWidth - 1;
                }
                if (playerCar.position.Y < 0)
                {
                    playerCar.y = 10;
                }
                if (playerCar.position.Y > graphics.PreferredBackBufferHeight)
                {
                    playerCar.y = graphics.PreferredBackBufferHeight - 1;
                }
                playerCar.onRoad = map.OnRoad(this);
                copCar.Drive(playerCar.position,gameTime.TotalGameTime.Milliseconds);
            }
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (gamestate == 0)
            {
                spriteBatch.Draw(menu.background, menu.bgrectangle, Color.White);
                spriteBatch.DrawString(Font, "Press A to start!", new Vector2(600, 400), Color.White);
            }
            if (gamestate == 1)
            {
                // TODO: Add your drawing code here

                spriteBatch.Draw(map.map, map.picPos, Color.White);
                spriteBatch.Draw(playerCar.texture, playerCar.position, null, Color.White, (playerCar.angle + playerCar.drift), Vector2.Zero, 1.0f, SpriteEffects.None, 0);

                spriteBatch.Draw(copCar.texture, new Vector2(copCar.x, copCar.y), null, Color.White, copCar.angle, new Vector2(0, playerCar.texture.Height / 2), 1.0f, SpriteEffects.None, 0);
                if (copCar.copDest.Count > 0)
                {
                    Vector2 V = (Vector2)copCar.copDest[0];
                    spriteBatch.Draw(copCar.texture, new Rectangle((int)V.X, (int)V.Y, 5, 5), Color.Blue);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
