using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PongGame
{
    class PoolManager
    {
        private static List<Ball> inactiveBall = new List<Ball>();
        private static List<Ball> activeBall = new List<Ball>();

        //Laver et nyt object
        public static Ball CreateBall()
        {
            lock(inactiveBall)
            {
                if (inactiveBall.Count != 0)
                {
                    Ball obj = inactiveBall[0];
                    activeBall.Add(obj);
                    inactiveBall.RemoveAt(0);
                    return obj;
                }
                else
                {
                    Ball obj = new Ball(new Vector2(RandomPicker.Rnd.Next(-1, 2), RandomPicker.Rnd.Next(-4, 5)));
                    activeBall.Add(obj);
                    return obj;
                }
            }
        }
        public static void ReleaseBallObject(Ball obj)
        {
            lock (inactiveBall)
            {
                CleanUpBall(obj);
                inactiveBall.Add(obj);
                activeBall.Remove(obj);
            }
        }
        private static void CleanUpBall(Ball obj)
        {
            obj.Velocity = Vector2.Zero;
        }
    }
}
