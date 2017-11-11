using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01.Core
{
    class Fleche:GameObject
    {
        //instances de la classe
        private int _sens;//indique le sens dans lequel va la fleche (sera expliqué plus bas en détail)
        private Rectangle _affichage;//rectangle gérant la position de la fleche
        private int _height;//hauteur
        private int _width;//longueur

        //Constructeurs
        public Fleche()
        {

        }
        public Fleche(Vector2 position, Vector2 select, int sens)
        {
            //select.X et select.Y indique la position à laquelle commence la sélection d'un élément dans l'image
            this._source = new Rectangle(
             (int)select.X,
             (int)select.Y,
             32,
             32);
            _position = position;
            this._affichage = new Rectangle(
             (int)_position.X,
             (int)_position.Y,
             32,
             32);
            _sens = sens;
        }

        //Méthodes 

        //Fonction qui dessine une partie de fleche(début,milieu,fin)
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, Vector2 pos_perso, int spec)//spec prend 1 si la fleche est le début et 2 si fin
        {
            //source est vide
            Rectangle source = new Rectangle(0 * _width, 0 * _height, 0, 0);

            if (spec == 1)//début de fleche
            {
                //explication du sens (représente des fleches directionnelles):
                //   8
                //4     6
                //   2
                if (_sens == 6)//droite
                {
                    source = new Rectangle(0*_width, 0*_height, _width, _height);
                }
                else if (_sens == 2)//bas
                {
                    source = new Rectangle(1*_width, 0*_height, _width, _height);
                }
                else if (_sens == 8)//haut
                {
                    source = new Rectangle(0*_width, 1*_height, _width, _height);
                }
                else if (_sens == 4)//gauche
                {
                    source = new Rectangle(1*_width, 1*_height, _width, _height);
                }
                //affichage du début de flèche sous l'unité
                spriteBatch.Draw(Texture, new Rectangle((int)pos_perso.X, (int)pos_perso.Y, 32, 32),source, Color.White);
            }
            else if (spec == 2)//fin de fleche
            {
                if (_sens == 6)//droite
                {
                    source = new Rectangle(6 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 2)//bas
                {
                    source = new Rectangle(7 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 8)//haut
                {
                    source = new Rectangle(6 * _width, 1 * _height, _width, _height);
                }
                else if (_sens == 4)//gauche
                {
                    source = new Rectangle(7 * _width, 1 * _height, _width, _height);
                }
                Console.WriteLine("pos_fleche:(" + _affichage.X + "," + _affichage.Y+")");
                Console.WriteLine("taille_fleche:(" + _affichage.Height + "," + _affichage.Width + ")");
                //affichage de la flèche
                spriteBatch.Draw(Texture, _affichage, source, Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, _affichage, _source, Color.White);
            }
        }

        //méthodes set

        //Fonction qui initialise la sélection dans l'image en fonction du sens
        public void setSource(int sens,int width=32,int height=32)
        {
            _width = width;
            _height = height;
            _sens = sens;
            _source.Width = _width;
            _source.Height = _height;

            _affichage.Height = _height;
            _affichage.Width = _width;
            switch (sens)
            {
                case 8://on va en haut
                    _source.X = 2 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 86://en était aller en haut et on tourne à droite
                    _source.X = 4 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 84://en était aller en haut et on tourne à gauche
                    _source.X = 5 * _width;
                    _source.Y = 0 * _height;
                    break;

                case 2://on va en bas
                    _source.X = 2 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 26://en était aller en bas et on tourne à droite
                    _source.X = 4 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 24://en était aller en bas et on tourne à gauche
                    _source.X = 5 * _width;
                    _source.Y = 1 * _height;
                    break;

                case 6://on va à droite
                    _source.X = 3 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 68://en était aller à droite et va en haut
                    _source.X = 5 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 62://en était aller à droite et va en bas
                    _source.X = 5 * _width;
                    _source.Y = 0 * _height;
                    break;

                case 4://on va à gauche
                    _source.X = 3 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 48://en était aller à gauche et va en haut
                    _source.X = 4 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 42://en était aller à gauche et va en bas
                    _source.X = 4 * _width;
                    _source.Y = 0 * _height;
                    break;
            }
        }

        public void setPosition(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
            this._affichage = new Rectangle(x,
                y,
                _width,
                _height);
            Console.WriteLine("pos_fleche:(" + _affichage.X + "," + _affichage.Y + ")");
        }

        new public void setPosition(Vector2 position)
        {
            _position = position;
            this._affichage = new Rectangle((int)position.X,
                (int)position.Y,
                _width,
                _height);
            Console.WriteLine("pos_fleche:(" + _affichage.X + "," + _affichage.Y + ")");
        }

        public void setSens(int sens)
        {
            _sens = sens;
        }

        //méthode get

        public int getSens()
        {
            return _sens;
        }
    }
}
