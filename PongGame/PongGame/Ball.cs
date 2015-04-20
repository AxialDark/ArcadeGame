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
        private bool collidedWithPlayer1 = false;
        private bool collidedWithPlayer2 = false;

        public Ball(Vector2 position)
            : base(position)
        {
            this.Position = position;
            this.velocity = new Vector2(RandomPicker.Rnd.Next(-1, 2), RandomPicker.Rnd.Next(-4, 5));
            this.speed = 750;
            this.layer = 0.0f;
            if (this.velocity.X == 0)
            {
                if (RandomPicker.Rnd.Next(2) == 0)
                {
                    this.velocity.X = -1;
                }
                else
                {
                    this.velocity.X = 1;
                }
            }
            LoadContent(GameWorld.myContent);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"pongBall");

            CreateAnimation("MoveBall", 3, 0, 0, 20, 20, new Vector2(0, 0), 3);
            CreateAnimation("BigBall", 3, 40, 0, 30, 30, Vector2.Zero, 1);
            CreateAnimation("SmallBall", 3, 0, 0, 10, 10, Vector2.Zero, 1);
            PlayAnimation("MoveBall");

            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime)
        {
            velocity.Normalize();
            velocity *= speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += (velocity * deltaTime);

            //SpawnNewBall();
            HandlePoint();

            base.Update(gameTime);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Obstacles)
            {
                Obstacles tempObstacle = other as Obstacles;
                if (!tempObstacle.IsMiddleLine)
                    this.velocity.Y *= -1;
            }
            if (other is Player && !collidedWithPlayer1 && !collidedWithPlayer2)
            {
                lastHitPlayer = other as Player;
                lastHitPlayer.Velocity = new Vector2(0, 0);
                if (lastHitPlayer.IsFirstPlayer)
                    collidedWithPlayer1 = true;
                else if (!lastHitPlayer.IsFirstPlayer)
                    collidedWithPlayer2 = true;

                float midBall = (this.CollisionRect.Y + this.CollisionRect.Height) - (this.CollisionRect.Height / 2);
                float midPlayer = (lastHitPlayer.CollisionRect.Y + lastHitPlayer.CollisionRect.Height) - (lastHitPlayer.CollisionRect.Height / 2);

                float deltaY = midBall - midPlayer;
                float lastDir = this.velocity.X;

                this.velocity = new Vector2(lastDir * -1, (deltaY) * 10);
            }
            if (other is PickUp)
            {
                HandlePickUp(other as PickUp);
            }
        }
        public override void ExitCollision(GameObject other)
        {
            if (other is Player)
            {
                Player temp = other as Player;
                if (temp.IsFirstPlayer)
                    collidedWithPlayer1 = false;
                else if (!temp.IsFirstPlayer)
                    collidedWithPlayer2 = false;
            }
        }
        private void HandlePoint()
        {
            if (this.Position.X > GameWorld.windowWidth)
            {
                GameWorld.ObjectsToRemove.Add(this);
                PoolManager.ReleaseBallObject(this);
                GameWorld.Player1Score++;
                //SpawnNewBall();
            }
            else if (this.Position.X < -20)
            {
                GameWorld.ObjectsToRemove.Add(this);
                PoolManager.ReleaseBallObject(this);
                GameWorld.Player2Score++;
                //SpawnNewBall();
            }
        }
        public static void SpawnNewBall()
        {
            int ballCount = 0;
            foreach (GameObject go in GameWorld.Objects)
            {
                if (go is Ball)
                    ballCount++;
            }
            if (ballCount <= 0)
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
                    foreach (GameObject go in GameWorld.Objects)
                    {
                        if (go is Obstacles)
                        {
                            Obstacles temp = go as Obstacles;
                            temp.HandlePickUp(pickUp);
                        }
                            
                    }
                    UsePickUp(pickUp);
                    break;

                case PickUpType.BigBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
                    break;

                case PickUpType.SmallBall:
                    GameWorld.ObjectsToRemove.Add(pickUp);
                    UsePickUp(pickUp);
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
                    this.speed += 300;
                    break;
                case PickUpType.MultiBall:
                    GameWorld.NewObjects.Add(PoolManager.CreateBall());
                    break;
                case PickUpType.SplitAndSlowBall:
                    float savedDir = this.Velocity.X;
                    Ball extraBall = PoolManager.CreateBall();
                    extraBall.Velocity = new Vector2(savedDir, -10);
                    extraBall.Position = this.Position;
                    this.Velocity = new Vector2(savedDir, 10);
                    this.speed -= 100;
                    extraBall.speed -= 100;
                    GameWorld.NewObjects.Add(extraBall);
                    break;
                case PickUpType.BigBall:
                    PlayAnimation("BigBall");
                    break;
                case PickUpType.SmallBall:
                    PlayAnimation("SmallBall");
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
