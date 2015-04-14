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
        private Ball ball;
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
        public Player(Vector2 position, Rectangle rect, bool isFirstPlayer)
            : base(position, rect)
        {
            // Creates animations
            //CreateAnimation("IdleRight", 1, 743, 0, 38, 39, Vector2.Zero, 1);
            //CreateAnimation("RunUp", 6, 353, 0, 39, 40, Vector2.Zero, 6);

            //PlayAnimation("IdleRight");

            this.isFirstPlayer = isFirstPlayer;
            this.position = position;
            this.rect = rect;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"bowserSprites");

            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            HandleInput(Keyboard.GetState());
            velocity *= Speed;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += (velocity * deltaTime);

            base.Update(gameTime);
        }
        public override void OnCollision(GameObject other)
        {

        }
        public void HandleInput(KeyboardState keyState)
        {
            if (isFirstPlayer)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    PlayAnimation("RunUp");
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
            if (isFirstPlayer)
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
            else
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
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.BigBall)
                {
                    //TO something
                    ball.Scale = 3.0f;

                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.BigPlayer)
                {
                    this.Scale = 3.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.ColorChange)
                {
                    this.Color = new Color(
                         (byte)rnd.Next(0, 255),
                         (byte)rnd.Next(0, 255),
                         (byte)rnd.Next(0, 255));
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.FastBall)
                {
                    ball.Speed = 50.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.FastPlayer)
                {
                    this.Speed = 60.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.InverseControl)
                {
                    InverseControl(Keyboard.GetState());
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.MultiBall)
                {
                    //DO SOMETHING TO SPAWN MULTIABLE BALLS 
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.RotatingObstacle)
                {
                    //DO SOMETHING TO ROTATE OBSTACLE
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SlowPlayer)
                {
                    this.Speed = 10.0f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SmallBall)
                {
                    ball.Scale = 0.5f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SmallPlayer)
                {
                    this.Scale = 0.5f;
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SpawnObstacle)
                {
                    //DO SOMETHING TO SPAWNOBSTACLE
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.SplitAndSlowBall)
                {
                    ball.Speed = 20.0f;
                    //DO SOMETHING TO SPLIT THE BALL
                }
                if ((pickUp as PickUp).PickUpPowerUp == PickUpType.xScore)
                {
                    GameWorld.Player1Score += 2;
                    GameWorld.Player2Score += 2;
                }
            }
        }
    }
}
