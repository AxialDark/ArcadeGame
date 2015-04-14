using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class PickUp : GameObject
    {
        // Fields
        private bool hasEndTime;
        private int effectTime;
        private PickUpType type;
        
        // Properties
        public bool HasEndTime
        {
            get { return hasEndTime; }
        }
        public int EffectTime
        {
            get { return effectTime; }
        }
        public PickUpType Type
        {
            get { return type; }
        }

        // Constructor
        public PickUp(Vector2 position, Rectangle rect, bool hasEndTime, int effectTime, Type pickUpTime) : base(position)
        {

        }

        // Methods
        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }
    }
}
