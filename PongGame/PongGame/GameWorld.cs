﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameWorld : Game
    {
        // Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static List<GameObject> objects = new List<GameObject>();
        private static List<GameObject> newObjects = new List<GameObject>();
        private static List<GameObject> objectsToRemove = new List<GameObject>();
        private static int player1Score;
        private static int player2Score;
        public static int windowWidth;
        public static int windowHeight;
        public static SpriteFont sf;
        public static SpriteFont sprFont;
        public static ContentManager myContent;
        private Lazy<List<PickUp>> pickUps;        
        private DateTime pickUpDelay = DateTime.Now;
        private bool pickUpSpawned = false;

        // Properties
        public static List<GameObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }
        public static List<GameObject> NewObjects
        {
            get { return newObjects; }
            set { newObjects = value; }
        }
        public static List<GameObject> ObjectsToRemove
        {
            get { return objectsToRemove; }
            set { objectsToRemove = value; }
        }
        public static int Player1Score
        {
            get { return player1Score; }
            set { player1Score = value; }
        }
        public static int Player2Score
        {
            get { return player2Score; }
            set { player2Score = value; }
        }
        public List<PickUp> PickUps
        {
            get
            {
                return pickUps.Value;
            }

        }

        // Constructor
        public GameWorld() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            pickUps = new Lazy<List<PickUp>>(() => LoadPickUps());
        }

        // Methods

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myContent = Content;
            objects.Add(new Player(new Vector2(60, 250), true));
            objects.Add(new Player(new Vector2(Window.ClientBounds.Width - 85, 250), false));
            objects.Add(new Ball(new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2)));
            objects.Add(new Obstacles(new Vector2(0, 0), false));
            objects.Add(new Obstacles(new Vector2(0, Window.ClientBounds.Height - 20), false));
            for (int i = 20; i < 520; i += 70)
            {
                objects.Add(new Obstacles(new Vector2(Window.ClientBounds.Width / 2 - 15, i), true));    
            }
                 
            IsMouseVisible = true;
            //graphics.IsFullScreen = true;
            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;
            //graphics.ApplyChanges();
            base.Initialize();
            //SpawnPickUp();
            
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sf = Content.Load<SpriteFont>(@"NewFont");
            sprFont = Content.Load<SpriteFont>(@"myFont");

            PreLoader.LoadTextures(Content);
            
            // TODO: use this.Content to load your game content here
            foreach (GameObject obj in objects)
            {
                obj.LoadContent(Content);
            }

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            objects.AddRange(newObjects);
            newObjects.Clear();

            foreach(GameObject dead in objectsToRemove)
            {
                objects.Remove(dead);
            }
            objectsToRemove.Clear();
            
            foreach (GameObject obj in objects)
            {
                obj.Update(gameTime);
            }
            SpawnPickUp();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (GameObject go in objects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(sf, player1Score.ToString(), new Vector2(8, 0), Color.Black);
            spriteBatch.DrawString(sf, player2Score.ToString(), new Vector2(Window.ClientBounds.Width - 20, 0), Color.Black);
            spriteBatch.DrawString(sf, windowWidth.ToString(), new Vector2(Window.ClientBounds.Width / 2 - 30, 0), Color.Black);
            spriteBatch.DrawString(sf, windowHeight.ToString(), new Vector2(Window.ClientBounds.Width / 2 + 30, 0), Color.Black);
            spriteBatch.DrawString(sf, gameTime.TotalGameTime.Seconds.ToString(), new Vector2(Window.ClientBounds.Width / 2, 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public List<PickUp> LoadPickUps()
        {
            List<PickUp> tempPickUp = new List<PickUp>() 
            {
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.BigBall),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.BigPlayer),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.ColorChange),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.FastBall),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.FastPlayer),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.InverseControl),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.MultiBall),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.RotatingObstacle),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.SlowPlayer),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.SmallBall),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.SmallPlayer),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.SpawnObstacle),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.SplitAndSlowBall),
              new PickUp(new Vector2(RandomPicker.Rnd.Next(80, Window.ClientBounds.Width - 200), RandomPicker.Rnd.Next(20, Window.ClientBounds.Height - 20)), false, 0, PickUpType.xScore)
            };


            return tempPickUp;
        }

        private void SpawnPickUp()
        {
            if (pickUpDelay <= DateTime.Now)
            {
                GameWorld.NewObjects.Add(PickUps[RandomPicker.Rnd.Next(0, 13)]);
                pickUpSpawned = true;
            }
            if (pickUpSpawned)
            {
                pickUpSpawned = false;
                pickUpDelay = DateTime.Now.AddSeconds(RandomPicker.Rnd.Next(5, 50));
            }
        }
    }
}
