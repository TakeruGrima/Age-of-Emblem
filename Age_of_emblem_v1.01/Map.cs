using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01
{
    class Map
    {
        //instances de la classe:
        private Vector2 _position;//stocke la position de la case
        private int _pass;//indique si une unité peut marcher dessus(0 dans ce cas si c'est une unité de base
        //dans le cas contraire, ne pas modifier de méthode mais creer une classe héréditaire

        //constructeur par défaut
        public Map()
        {
        }
        //constructeur
        public Map(Vector2 position,int pass)
        {
            _position = position;
            _pass = pass;
        }

        //les méthodes get
        public Vector2 getPosition()
        {
            return _position;
        }
        public int getPass()
        {
            return _pass;
        }

        //les méthodes set
        public void setPass(int pass)
        {
            _pass = pass;
        }

        public void setPosition(Vector2 position)
        {
            _position = position;
        }

        public void setPosition(int x,int y)
        {
            _position.X = x;
            _position.Y = y;
        }


        //les méthodes de traitement

        //Le test du passage
        public bool ifPass()
        {
            if(_pass==0)
            {
                return true;
            }
            return false;
        }
        
        //Le test de la position égal(argument x et y)
        public bool ifPositionEgal(int x,int y)
        {
            if(x==_position.X && y==_position.Y)
            {
                return true;
            }
            return false;
        }

        //le test de la position égal(argument position)
        public bool ifPositionEgal(Vector2 position)
        {
            if (position==_position)
            {
                return true;
            }
            return false;
        }
    }
}