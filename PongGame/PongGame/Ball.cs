using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class Ball : GameObject
    {
        // Fields
        private Player lastHitPlayer;

        // Properties


        // Constructor
        public Ball(Vector2 position)
            : base(position)
        {
            this.Position = position;
            this.velocity = new Vector2(RandomPicker.Rnd.Next(-1, 2), RandomPicker.Rnd.Next(-4, 5));
            if (this.velocity.X == 0)
            {
                if (RandomPicker.Rnd.Next(2) == 0)
                {
                    //PlayAnimation("MoveLeftBall");
                    this.velocity.X = -1;
                }
                else
                {
                    //PlayAnimation("MoveRightBall");
                    this.velocity.X = 1;
                }
            }
            LoadContent(GameWorld.myContent);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"pongBall");

            CreateAnimation("MoveLeftBall", 3, 0, 0, 20, 20, new Vector2(0, 0), 3);
            CreateAnimation("MoveRightBall", 3, 40, 0, 20, 20, new Vector2(0, 0), 3);
            PlayAnimation("MoveLeftBall");

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Normalize();
            velocity *= Speed + speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += (velocity * deltaTime);

            HandlePoint();

            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Obstacles)
            {
                this.velocity.Y *= -1;
            }
            if (other is Player)
            {
                lastHitPlayer = other as Player;
                //this.velocity.X *= -1;
                //float deltaYOrigin = lastHitPlayer.Origin.Y - this.Origin.Y;
                float deltaYPosition = lastHitPlayer.Position.Y - this.position.Y;
                
                float lastDir = this.velocity.X;

                this.velocity = new Vector2(lastDir * -1, (-deltaYPosition) * 5);
                if (velocity.Y < 0)
                {
                    velocity.Y *= 2;
                }
            }
            if (other is PickUp)
            {
                HandlePickUp(other as PickUp);
            }
        }

        private void HandlePoint()
        {
            if (this.Position.X > GameWorld.windowWidth)
            {
                PoolManager.ReleaseBallObject(this);
                GameWorld.ObjectsToRemove.Add(this);
                GameWorld.Player1Score++;
                SpawnNewBall();
            }
            else if (this.Position.X < -20)
            {
                PoolManager.ReleaseBallObject(this);
                GameWorld.ObjectsToRemove.Add(this);
                GameWorld.Player2Score++;
                SpawnNewBall();
            }
        }
        private void SpawnNewBall()
        {
            GameWorld.NewObjects.Add(PoolManager.CreateBall());
        }
        private void HandlePickUp(PickUp pickUp)
        {
            switch (pickUp.PickUpPowerUp)
            {
                case PickUpType.SlowPlayer:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.FastPlayer:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.FastBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);                    
                    break;

                case PickUpType.SpawnObstacle:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    break;

                case PickUpType.MultiBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
                    break;

                case PickUpType.BigPlayer:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.SmallPlayer:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.xScore:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.SplitAndSlowBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
                    break;

                case PickUpType.ColorChange:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;

                case PickUpType.BigBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
                    break;

                case PickUpType.SmallBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
                    break;

                case PickUpType.RotatingObstacle:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    break;

                case PickUpType.InverseControl:
                    if (lastHitPlayer != null)
                    {
                        lastHitPlayer.HandlePickUp(pickUp);
                        GameWorld.ObjectsToRemove.Add(pickUp);
                    }
                    break;
                default:
                    break;
            }
        }
        private void UsePickUp(PickUp pickUp)
        {
            switch (pickUp.PickUpPowerUp)
            {
                case PickUpType.FastBall:
                    this.speed += 100;
                    break;
                case PickUpType.MultiBall:
                    GameWorld.NewObjects.Add(PoolManager.CreateBall());
                    break;
                case PickUpType.SplitAndSlowBall:
                    float savedDir = this.Velocity.X;
                    Ball extraBall = PoolManager.CreateBall();
                    extraBall.Velocity = new Vector2(savedDir, -2);
                    extraBall.Position = this.Position;
                    this.Velocity = new Vector2(savedDir, 2);
                    this.speed -= 50;
                    extraBall.speed -= 50;
                    GameWorld.NewObjects.Add(extraBall);
                    break;
                case PickUpType.BigBall:
                    this.Scale += 1f;
                    break;
                case PickUpType.SmallBall:
                    this.Scale -= 0.5f;
                    break;

                case PickUpType.ColorChange:
                    this.Color = new Color(
                         (byte)RandomPicker.Rnd.Next(0, 255),
                         (byte)RandomPicker.Rnd.Next(0, 255),
                         (byte)RandomPicker.Rnd.Next(0, 255));
                    break;

                default:
                    break;
            }
        }
    }
}
