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
        private bool inverseControl = false;
        //private Player player;
        private Random rnd = new Random();

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
        public Player(Vector2 position, bool isFirstPlayer)
            : base(position)
        {
            //Creates animations
            CreateAnimation("IdlePlayer", 1, 0, 0, 15, 70, Vector2.Zero, 1);
            CreateAnimation("BigPlayer", 1, 210, 0, 15, 140, Vector2.Zero, 1);
            CreateAnimation("SmallPlayer", 1, 210, 0, 15, 35, Vector2.Zero, 1);
            PlayAnimation("IdlePlayer");

            this.isFirstPlayer = isFirstPlayer;
            this.speed = 450;
            this.Position = position;
        }

        // Methods 
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");

            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;
            if (!inverseControl)
                HandleInput(Keyboard.GetState());
            else if (inverseControl)
                InverseControl(Keyboard.GetState());
            velocity *= Speed;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (velocity * deltaTime);
            base.Update(gameTime);
        }
        public override void OnCollision(GameObject other)
        {            
            if (other is Obstacles)
            {
                if (position.Y <= 20)
                {
                    position.Y = 20;
                }
                else if (position.Y + CollisionRect.Height > other.Position.Y)
                {
                    position.Y = other.Position.Y - CollisionRect.Height;
                }
            }
        }

        public void HandleInput(KeyboardState keyState)
        {
            if (isFirstPlayer)
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
            else
            {
                if (keyState.IsKeyDown(Keys.Up))
                {
                    velocity += new Vector2(0, -1);
                }
                else if (keyState.IsKeyDown(Keys.Down))
                {
                    velocity += new Vector2(0, 1);
                }
            }

        }
        private void InverseControl(KeyboardState keyState)
        {
            if (!isFirstPlayer)
            {
                if (keyState.IsKeyDown(Keys.Down))
                {
                    velocity += new Vector2(0, -1);
                }
                else if (keyState.IsKeyDown(Keys.Up))
                {
                    velocity += new Vector2(0, 1);
                }
            }
            else if (isFirstPlayer)
            {
                if (keyState.IsKeyDown(Keys.S))
                {
                    velocity += new Vector2(0, -1);
                }
                else if (keyState.IsKeyDown(Keys.W))
                {
                    velocity += new Vector2(0, 1);
                }
            }
        }
        public void HandlePickUp(GameObject pickUp)
        {
            if (pickUp is PickUp)
            {
                #region PickUp's
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.BigPlayer)
                {
                    PlayAnimation("BigPlayer");
                    //this.Scale = 2.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.ColorChange)
                {
                    this.Color = new Color(
                         (byte)RandomPicker.Rnd.Next(0, 255),
                         (byte)RandomPicker.Rnd.Next(0, 255),
                         (byte)RandomPicker.Rnd.Next(0, 255));
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.FastPlayer)
                {
                    this.Speed += 300.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.InverseControl)
                {
                    InverseControl(Keyboard.GetState());
                    inverseControl = true;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SlowPlayer)
                {
                    this.Speed -= 250.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SmallPlayer)
                {
                    PlayAnimation("SmallPlayer");
                    //this.Scale = 0.5f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.xScore)
                {
                    if (this.isFirstPlayer)
                        GameWorld.Player1Score += 2;
                    else if (!this.isFirstPlayer)
                        GameWorld.Player2Score += 2;
                }
                #endregion
            }
        }

        public override void ExitCollision(GameObject other)
        {
        }
    }
}
