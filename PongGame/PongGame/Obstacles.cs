﻿using System;
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
            this.position = position;
            this.origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"white");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
