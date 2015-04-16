using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class Obstacles : GameObject
    {
        // Fields


        // Properties


        // Constructor
        public Obstacles(Vector2 position) : base(position)
        {
            this.Position = position;
            this.Origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");

            CreateAnimation("IdleObstacle", 1, 0, 1, 1000, 20, new Vector2(0, 0), 1);

            PlayAnimation("IdleObstacle");

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }

        public override void ExitCollision(GameObject other)
        {
        }
    }
}
