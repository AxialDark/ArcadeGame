using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class Animation
    {
        // Fields
        private Vector2 offset;
        private float fps;
        private Rectangle[] rectangles;

        // Properties
        public Vector2 Offset
        {
            get { return offset; }
        }
        public float Fps
        {
            get { return fps; }
        }
        public Rectangle[] Rectangle
        {
            get { return rectangles; }
        }

        // Constructor
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, Vector2 offset, float fps)
        {
            rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

            this.fps = fps;
            this.offset = offset;
        }

        // Methods
    }
}
