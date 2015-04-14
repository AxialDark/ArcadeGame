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
        public Obstacles(Vector2 position, Rectangle rect) : base(position, rect)
        {
            this.position = position;
            this.rect = rect;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
