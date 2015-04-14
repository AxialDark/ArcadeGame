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
        public Ball(Vector2 position, Rectangle rect)
            : base(position, rect)
        {
            this.position = position;
            this.rect = rect;
            this.Speed = 50;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.velocity = new Vector2(RandomPicker.Rnd.Next(-1, 2), RandomPicker.Rnd.Next(-4, 5));
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"");
        }

        public override void Update(GameTime gameTime)
        {
            velocity *= Speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += (velocity * deltaTime);

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
    }
}
