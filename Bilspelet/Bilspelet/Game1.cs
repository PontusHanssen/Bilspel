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
        
        float turn = 0.06f;
        Map map;
        public AI copCar;
        public AI playerCar;
        Physics Physics = new Physics();
        public int laps=0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
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
            copCar = new AI(0, 0, 10, 10, 10, 10, Content.Load<Texture2D>(@"Textures/red"), true);
            playerCar = new AI(0, 100, 10, 5, 0, 100, Content.Load<Texture2D>(@"Textures/blue"), true);
            map = new Map(this);
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
            GamePadState GPad1 = GamePad.GetState(PlayerIndex.One);
           

          
            //Kör i cirklar.
            //PlayerCar.Angle += turn;
            copCar.x += 1;
            map.SetCamera(this);
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
            playerCar.speed = Physics.Acceleration(playerCar.speed, playerCar.friction, playerCar.acceleration, playerCar.mass, playerCar.topspeed);
            playerCar.x += (float)Math.Cos(playerCar.angle) * playerCar.speed;
            playerCar.y += (float)Math.Sin(playerCar.angle) * playerCar.speed;

            playerCar.angle += Physics.Turn(playerCar.speed, playerCar.friction, playerCar.mass, playerCar.position, playerCar.prevpos, GPad1.ThumbSticks.Left.X, playerCar.wheelbase);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(map.map, map.picPos, Color.White);
            spriteBatch.Draw(playerCar.texture, playerCar.position, null, Color.White, playerCar.angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(copCar.texture, copCar.position, null, Color.White, copCar.angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(copCar.texture, new Rectangle((int)map._checkpoint.X,(int)map._checkpoint.Y,1,500), Color.Brown);
            spriteBatch.Draw(copCar.texture, new Rectangle((int)map._goal.X, (int)map._goal.Y, 1, 500), Color.Brown);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
