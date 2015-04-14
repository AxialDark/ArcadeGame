using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private int player1Score;
        private int player2Score;

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
        public int Player1Score
        {
            get { return player1Score; }
            set { player1Score = value; }
        }
        public int Player2Score
        {
            get { return player2Score; }
            set { player2Score = value; }
        }

        // Constructor
        public GameWorld() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            newObjects.Add(new Player(new Vector2(10, 250), true));
            newObjects.Add(new Player(new Vector2(Window.ClientBounds.Width - 10, 250), false));
            newObjects.Add(new Ball(new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2)));

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

            foreach (GameObject go in newObjects)
            {
                go.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
