using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    class Player : GameObject
    {
        // Fields
        private bool hasPowerUp;
        private bool isFirstPlayer;
        private DateTime powerUpEnd;

        // Properties
        public bool HasPowerUp
        {
            get { return hasPowerUp; }
        }
        public bool IsFirstPlayer
        {
            get { return isFirstPlayer; }
        }

        // Constructor
        public Player(Vector2 position, Rectangle rect, bool isFirstPlayer) : base(position)
        {
            // Creates animations
            CreateAnimation("IdleRight", 1, 743, 0, 38, 39, Vector2.Zero, 1);
            CreateAnimation("RunUp", 6, 353, 0, 39, 40, Vector2.Zero, 6);

            PlayAnimation("IdleRight");
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"bowserSprites");
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            HandleInput(Keyboard.GetState());
            velocity *= speed;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += (velocity * deltaTime);

            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {

        }

        public void HandleInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }
        }

        public void HandlePickUp(GameObject pickUp)
        {

        }
    }
}
