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
        private PickUpType pickUpPowerUp;
        
        // Properties
        public bool HasEndTime
        {
            get { return hasEndTime; }
        }
        public int EffectTime
        {
            get { return effectTime; }
        }
        public PickUpType PickUpPowerUp
        {
            get { return pickUpPowerUp; }
            set { pickUpPowerUp = value; }
        }

        // Constructor
        public PickUp(Vector2 position, bool hasEndTime, int effectTime, PickUpType pickUpPowerUp) : base(position)
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

        public override void ExitCollision(GameObject other)
        {
        }
    }
}
