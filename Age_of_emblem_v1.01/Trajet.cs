using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Age_of_emblem_v1._01.Core;

namespace Age_of_emblem_v1._01
{
    class Trajet
    {
        //Instances de classe
        private List<Fleche> _trajet;//liste de bout de fleche
        private bool _vide;//indique si la fleche est vide
        private Vector2 _posPerso;//position du perso
        private Vector2 _prec;//position précédente de la fleche
        private int _taille;//taille de la liste
        private int _width;
        private int _height;
        private Texture2D _texture;//image

        //Constructeur
        public Trajet(int width=32,int height=32)//taille d'une case en pixel sur l'écran
        {
            _vide = true;
            _taille = 0;
            _width = width;
            _height = height;
        }

        //Méthode
        //Méthode pour afficher le trajet avec les fleches
        public void Draw(SpriteBatch spriteBatch)
        {
            _trajet[0].Draw(spriteBatch, _texture, _posPerso, 1);//affiche le début de la flèche sous le perso
            for(int i=0;i<_taille-1;i++)
            {
                _trajet[i].Draw(spriteBatch, _texture, _posPerso, 0);//affiche un bout de la fleche
            }
            _trajet.Last().Draw(spriteBatch, _texture, _posPerso, 2);//affiche la fin de la fleche
        }

        //Méhode set
        public void setVide(bool vide)
        {
            _vide = vide;
        }

        public void setTaille(int taille)
        {
            _taille = taille;
        }

        public void setTexture(Texture2D texture)
        {
            _texture = texture;
        }

        
        //Méthode pour ajouter un chemin
        public void addChemin(Vector2 posInTab, Unit perso, World world)
        {
            bool inMove =false;//par défaut faux, si le curseur est dans la zone de déplacement ce sera true

            //parcourt la map pour savoir si le curseur est sur un obstacle en cas d'obstacle on sort de la fonction
            foreach (Map curr2 in world.getMap())
            {
                if (curr2.ifPositionEgal(posInTab) == true)
                {
                    if (curr2.ifPass() == false)
                    {
                        Console.WriteLine("interdit!");
                        return;
                    }
                }
            }

            //parcourt la zone de déplacement dans le cas où il s'agit d'une case infranchissable en sort de la fonction
            foreach (Map curr2 in perso.getMove())
            {
                if (curr2.ifPositionEgal(posInTab) == true)
                {
                    inMove = true;//le curseur est dans la zone
                    if (curr2.ifPass() == false)
                    {
                        Console.WriteLine("passe pas");
                        return;
                    }
                }
            }

            if (_vide==true)//si le joueur commence à définir le trajet de son personnage
            {
                Console.WriteLine("_vide=true");
                _posPerso.X = perso.getPosInTab().X;
                _posPerso.Y = perso.getPosInTab().Y;

                Fleche curr = new Fleche();

                curr.setPosition(posInTab);

                if(inMove)//le curseur est dans la zone
                {
                    _trajet = new List<Fleche>();//on initialise une nouvelle liste

                    Console.WriteLine("cursor.x:" + posInTab.X + "player.X:" + _posPerso.X);
                    Console.WriteLine("cursor.y:" + posInTab.Y + "player.y:" + _posPerso.Y);
                    if (posInTab.X > _posPerso.X)//droite
                    {
                        curr.setSource(6);
                    }
                    else if (posInTab.X < _posPerso.X)//gauche
                    {
                        curr.setSource(4);
                    }
                    else if (posInTab.Y > _posPerso.Y)//bas
                    {
                        curr.setSource(2);
                    }
                    else if (posInTab.Y < _posPerso.Y)//haut
                    {
                        curr.setSource(8);
                    }
                    else
                    {
                        Console.WriteLine("Wut");
                        return;
                    }
                    _trajet.Add(curr);//on ajoute la fleche à la liste
                    _vide = false;//le trajet n'est plus vide
                    _prec = posInTab;
                    _taille++;//incremente la taille du trajet
                    Console.WriteLine("Add");
                }
            }
            else
            {
                Fleche curr = new Fleche();

                curr.setPosition(posInTab);

                if (inMove)
                {
                    if (posInTab.X > _prec.X)//droite
                    {
                        if (_trajet.Last().getSens() != 6)
                        {
                            //on modifie le bout de fleche précédent pour quel forme un virage
                            _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 6);
                        }
                        curr.setSource(6);
                    }
                    else if (posInTab.X < _prec.X)//gauche
                    {
                        if (_trajet.Last().getSens() != 4)
                        {
                            //on modifie le bout de fleche précédent pour quel forme un virage
                            _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 4);
                        }
                        curr.setSource(4);
                    }
                    else if (posInTab.Y > _prec.Y)//bas
                    {
                        if (_trajet.Last().getSens() != 2)
                        {
                            //on modifie le bout de fleche précédent pour quel forme un virage
                            _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 2);
                        }
                        curr.setSource(2);
                    }
                    else if (posInTab.Y < _prec.Y)//haut
                    {
                        if (_trajet.Last().getSens() != 8)
                        {
                            //on modifie le bout de fleche précédent pour quel forme un virage
                            _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 8);
                        }
                        curr.setSource(8);
                    }
                    else
                    {
                        Console.WriteLine("Wut");
                        return;
                    }

                    _trajet.Add(curr);//on ajoute le bout de fleche
                    _prec = posInTab;
                    _taille++;
                }
            }
        }
    }
}
