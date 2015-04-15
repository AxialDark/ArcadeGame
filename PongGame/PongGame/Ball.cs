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


        // Properties


        // Constructor
        public Ball(Vector2 position) : base(position)
        {
            this.Position = position;
            this.speed = 200;
            this.origin = new Vector2(rect.Width / 2, rect.Height / 2);
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
            texture = content.Load<Texture2D>(@"ballSheet");
            
            CreateAnimation("MoveLeftBall", 3, 0, 0, 40, 40, new Vector2(0, 0), 3);
            CreateAnimation("MoveRightBall", 3, 40, 0, 40, 40, new Vector2(0, 0), 3);
            PlayAnimation("MoveLeftBall");

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Normalize();
            velocity *= Speed;

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
                this.velocity.X *= -1;
            }
        }

        private void HandlePoint()
        {
            if(this.Position.X > GameWorld.windowWidth)
            {
                GameWorld.ObjectsToRemove.Add(this);
                //PoolManager.ReleaseBallObject(this);
                GameWorld.Player1Score++;
                SpawnNewBall();
            }
            else if(this.Position.X < -20)
            {
                GameWorld.ObjectsToRemove.Add(this);
                //PoolManager.ReleaseBallObject(this);
                GameWorld.Player2Score++;
                SpawnNewBall();
            }
        }
        private void SpawnNewBall()
        {
            GameWorld.NewObjects.Add(PoolManager.CreateBall());
        }
    }
}
