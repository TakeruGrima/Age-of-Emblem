using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01.Core
{
    class Unit :GameObject
    {
        //Instances de la classe
        private int _rayon;//rayon de déplacement
        private bool _moveDef;// indique si la zone de déplacement est définie
        private int _mapHeight;//hauteur de la map
        private int _mapWidth;//longueur de la map
        private bool _deplace;//indique si l'unité va effectuer un déplacement
        private bool _onUnit;//variable indiquant que le curseur est sur l'unité

        private List<Map> _listVoisin;//liste utilisé pour la recherche des voisins d'une case(expliqué plus tard)

        private List<Map> _move = new List<Map>();//Liste contenant les cases se trouvant dans le champs de
        //déplacement de l'unité

        private Vector2 _posInTab;//la position de l'unité dans mais en 32*32 (pas la position réelle)

        private int _anim;//sens de l'animation dans l'image

        //Méthodes set
        public void setRayon(int rayon)
        {
            _rayon = rayon;
        }

        public void setTMap(Vector2 taille)
        {
            _mapWidth = (int)taille.X;
            _mapHeight = (int)taille.Y;
        }

        public void setTMap(int w,int h)
        {
            _mapWidth = w;
            _mapHeight = h;
        }

        public void setDeplace(bool deplace)
        {
            _deplace = deplace;
        }

        public void setMoveDef(bool moveDef)
        {
            _moveDef = moveDef;
        }

        public void setPosition(int i,int j)
        {
            _position.X = i * 32 - 16;
            _position.Y = j * 32 - 16;

            _posInTab.X = i*32;
            _posInTab.Y = j*32;
        }

        public void setOnUnit(bool onUnit)
        {
            _onUnit = onUnit;
        }

        public void setAnim(int anim)//défini le sens de l'animation dans l'image 
        {
            _anim = anim;
        }


        //Méthodes get
        public bool getDeplace()
        {
            return _deplace;
        }

        public bool getMoveDef()
        {
            return _moveDef;
        }

        public Vector2 getPosInTab()
        {
            return _posInTab;
        }

        public int getRayon()
        {
            return _rayon;
        }

        public List<Map> getMove()
        {
            return _move;
        }

        //méthode d'animation
        public void anim(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 0.2f)//défini le temps d'attente entre chaque frames
            {
                if(_onUnit==true)//Si le curseur est sur l'unité
                {
                    _source.X = 266;//affichage d'une animation spéciale
                }
                else//Sinon
                {
                    _source.X = 2;//animation standard
                }
                if(_source.Y==192)//derniere image de l'animation affiché
                {
                    _anim = 8;//on défini le sens de l'animation vers le haut
                }
                else if(_source.Y==64)//premiere image affiché
                {
                    _anim = 2;//on défini le sens de l'animation vers le bas
                }
                if (_anim == 2)//si l'animation va vers le bas
                {
                    _source.Y += 64;// 64 correspond à l'espace entre chaque partie de l'image
                }
                if(_anim==8)//si l'animation va vers le haut
                {
                    _source.Y -= 64;
                }
                _time = 0;//réinitialisation à 0
            }
        }

        //Méthode de Draw
        new public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), _source, Color.White);
        }

        //Méthode pass_possible: Détermine si l'unité peut passer sur la case
        private bool pass_possible(World world, int x, int y)
        {
            //la classe Distance contient une fonction qui détermine la distance en deux points
            if (new Distance(_posInTab, x, y).getValue()==_rayon)//si la distance entre l'unité et la case est égal
                //au rayon
            {
                AddToList(x, y, 1, _move);//on ajoute la case à _move mais en indiquant que l'unité ne peut pas
                //marcher dessus car elle est or de dans son champs de déplacement
                return false;//on return que le l'unité ne peut marcher sur cette case
            }
            foreach (Map curr in world.getMap())//On parcourt la map
            {
                if (curr.ifPositionEgal(x, y) == true)//si on trouve la case dans la map
                {
                    if (curr.ifPass() == false)//si la map indique que l'unité ne peut marcher sur cette case
                    {
                        AddToList(x, y,1, _move);//on ajoute la case à _move mais en indiquant que l'unité ne peut 
                        //pas marcher dessus
                        return false;//on return que le l'unité ne peut marcher sur cette case
                    }
                }
            }
            return true;//on return que l'unité peut marcher sur cette case
        }

        //Méthodes pour déterminé _move
            //Méthode voisin: détermine le voisin d'une case
        unsafe void voisin(int x, int y, World world)
        {
            _listVoisin = new List<Map>();//liste qui va contenir les voisins (4 voisins)

            bool inf_max_x32 = false, inf_max_y32 = false;

            if (x < _mapWidth - 32)//on teste si le voisin de la case sort de l'écran à droite
            {
                inf_max_x32 = true;//x pourra avoir un voisin à droite
            }
            if (y < _mapHeight - 32)//on teste si le voisin de la case sort de l'écran en bas
            {
                inf_max_y32 = true;//y pourra avoir un voisin en bas
            }

            Console.WriteLine("x: " + x);
            Console.WriteLine("y: " + y);
            Console.WriteLine("Taille ecran:" + _mapWidth + ";" + _mapHeight);

            //recherche des voisins dans les 4 directions
            Console.WriteLine("Recherche voisin:\n");
            if (x > 0 && pass_possible(world, x - 32, y) == true)/*recherche du voisin de gauche en vérifiant qu'il 
                ne sorte pas de l'écran & si l'unité peut y marcher*/
            {
                AddToList(x - 32, y, 0, _listVoisin);//mise du voisin de gauche dans la liste 
                Console.WriteLine("voisin gauche :(" + _listVoisin.Last().getPosition().X + ";" + _listVoisin.Last().getPosition().Y + ")");
            }
            if (inf_max_x32 == true && x >= 0 && pass_possible(world, x + 32, y) == true)/*recherche du voisin de 
                droite en vérifiant qu'il ne sorte pas de l'écran & si l'unité peut y marcher*/
            {
                AddToList(x + 32, y, 0, _listVoisin);//mise du voisin de droite dans la liste
                Console.WriteLine("voisin droite :(" + _listVoisin.Last().getPosition().X + ";" + _listVoisin.Last().getPosition().Y + ")");
            }
            if (y > 0 && pass_possible(world, x, y - 32) == true)/*recherche du voisin du haut en vérifiant qu'il 
                ne sorte pas de l'écran & si l'unité peut y marcher & si l'unité peut y marcher*/
            {
                AddToList(x, y-32, 0, _listVoisin);//mise du voisin du haut dans la liste
                Console.WriteLine("voisin haut :(" + _listVoisin.Last().getPosition().X + ";" + _listVoisin.Last().getPosition().Y + ")");
            }
            if (inf_max_y32 == true && y >= 0 && pass_possible(world, x, y + 32) == true)/*recherche du voisin du bas
                en vérifiant qu'il ne sorte pas de l'écran & si l'unité peut y marcher*/
            {
                AddToList(x, y + 32, 0, _listVoisin);//mise du voisin du bas dans la liste
                Console.WriteLine("voisin bas  :(" + _listVoisin.Last().getPosition().X + ";" + _listVoisin.Last().getPosition().Y + ")");
            }
        }

        //Méthode getGroupe: la fonction sert à définir le champs de déplacement de l'unité contenu dans _move
        unsafe public void getGroupe(int x, int y, World world)
        {
            Boolean trouve = false;// boolean qui indique si le voisin trouvé est déja dans le groupe ou pas
            int i, j;//compteurs

            i = x;
            j = y;
            Console.WriteLine("x:" + x + "y" + y);

            voisin(x, y, world);//fonction qui récupère la liste des voisins de (x,y)

            AddToList(x, y, 0, _move);//on ajoute la case dans _move

            Console.WriteLine("dernier voisin:(" + _listVoisin.Last().getPosition().X + ";" + _listVoisin.Last().getPosition().Y + ")");
            Console.WriteLine("foreach en dessous:");
            foreach (Map curr in _listVoisin)//on parcourt la liste des voisins
            {
                Console.WriteLine("Début foreach\n");
                i = (int)curr.getPosition().X;
                j = (int)curr.getPosition().Y;
                Console.WriteLine("x:" + x + " y:" + y);

                trouve = testexist(i, j);/*test si le voisin courant est déja dans le groupe renvoit false si 
		        non et true si ou */

                if (trouve == false)//si le voisin n'est pas encore dans le groupe
                {
                    getGroupe(i, j, world);/*on relance la fonction afin de voir si les voisins de la case courante
                    vont dans _move*/
                    Console.WriteLine("add_groupe");
                }
                Console.WriteLine("next");
            }
        }

        //méthode pour définir _move
        public void define_move(World world)
        {
            _move = new List<Map>();//initialisation

            getGroupe((int)_posInTab.X,(int)_posInTab.Y, world);//fontion getGroupe
            _moveDef = true;//le champs de déplacement de l'unité est défini
        }

        //méthode pour savoir si une case est dans le groupe
        unsafe Boolean testexist(int x, int y)
        {
            foreach (Map curr in _move)
            {
                if(curr.ifPositionEgal(x,y)==true)
                {
                    return true;
                }
            }
            return false;
        }

        //méthodes pour ajouter une case à _move
            //Ajout avec un vecteur
        public void AddToList(Vector2 position, int pass,List<Map> liste)
        {
            liste.Add(new Map(position, pass));//on donne la position de la case et si l'unité peut y marcher
        }
            //Ajout avec coordonnée
        public void AddToList(int x, int y, int pass, List<Map> liste)
        {
            liste.Add(new Map(new Vector2(x, y), pass));/*on donne la position de la case avec coordonnée et si
            l'unité peut y marcher*/
        }
    }
}
