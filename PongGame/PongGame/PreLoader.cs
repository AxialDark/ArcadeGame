using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PongGame
{
    static class PreLoader
    {
        // to add a new texture to the loader:
        // 1: Make a public static Texture with a variable name
        // 2: add the variable name in the loader funktion with the respective image
        // 3: in the object or script that needs the given texture, use PreLoader.[variable name] to get you texture :)


        public static Texture2D tempTest;
        public static Texture2D boxTexture;

        public static void LoadTextures(ContentManager content)
        {
            tempTest = content.Load<Texture2D>(@"red");
            boxTexture = content.Load<Texture2D>(@"CollisionTexture");
        }
    }
}
