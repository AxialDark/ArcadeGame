using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PongGame
{
    static class RandomPicker
    {
        private static Random rnd = new Random();

        public static Random Rnd
        {
            get { return RandomPicker.rnd; }
            set { RandomPicker.rnd = value; }
        }
    }
}
