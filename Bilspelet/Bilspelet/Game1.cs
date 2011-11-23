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

        Car PlayerCar;
        Physics Physics = new Physics();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            PlayerCar = new Car(100, 100, 10, Content.Load<Texture2D>("./Textures/car"), true);
            PlayerCar.Speed = 5;
            PlayerCar.Mass = 1000;
            PlayerCar.Friction = 2000;

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
            //Kör i cirklar.
            //PlayerCar.Angle += turn;

            //Testa bromsning
            if (PlayerCar.Speed > 0.01f)
            {
                PlayerCar.Speed = Physics.Brake(PlayerCar.Speed, PlayerCar.Friction, 1, PlayerCar.Mass);

            }
            else
            {
                turn = 0;
            }
            PlayerCar.X += (float)Math.Cos(PlayerCar.Angle) * PlayerCar.Speed;
            PlayerCar.Y += (float)Math.Sin(PlayerCar.Angle) * PlayerCar.Speed;


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

            spriteBatch.Draw(PlayerCar.Texture, PlayerCar.Position, null, Color.White, PlayerCar.Angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
