using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01
{
    class Distance
    {
        private int _value;// la distance entre 2 points
        
        //Constructeur
        public Distance(Vector2 Position, int x, int y)
        {
            Console.WriteLine("Position.X:" + Position.X);
            Console.WriteLine("Position.Y:" + Position.Y);
            Console.WriteLine("x:" + x);
            Console.WriteLine("y:" + y);
            _value = 0;//value initialisé à 0

            while (Position.X != x || Position.Y != y)
            {
                if (Position.X > x)
                {
                    x += 32;
                    _value++;
                }
                if (Position.X < x)
                {
                    x -= 32;
                    _value++;
                }
                if (Position.Y > y)
                {
                    y += 32;
                    _value++;
                }
                if (Position.Y < y)
                {
                    y -= 32;
                    _value++;
                }
            }
        }

        //Méthode get
        public int getValue()
        {
            return _value;
        }
    }
}
