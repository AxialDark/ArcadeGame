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
            this.pickUpPowerUp = pickUpPowerUp;
            LoadContent(GameWorld.myContent);
            this.layer = 0.5f;
        }

        // Methods
        public override void LoadContent(ContentManager content)
        {
            texture = PreLoader.tempTest;
            CreateAnimation("IdlePickUp", 1, 0, 0, 20, 20, new Vector2(0, 0), 1);
            PlayAnimation("IdlePickUp");
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.sprFont, pickUpPowerUp.ToString(), new Vector2(position.X, position.Y - 20), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
