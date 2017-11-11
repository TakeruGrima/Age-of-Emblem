using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Age_of_emblem_v1._01.Core
{
    class World : GameObject
    {
        //instances de la classe
        private string _mapName;//nom de la map sous forme exemple: "000.txt"
        private List<Map> _map = new List<Map>();//Liste contenant les cases de la map avec leur niveau de passage
        private Vector2 _tCase; //defini la hauteur et la largeur de la case

        //constructeur
        public World(string mapID,Vector2 tCase)
        {
            _mapName = mapID + ".txt";
            _tCase = tCase;

            TextReader reader;
            string line;
            reader = new StreamReader(_mapName);

            _map = new List<Map>();

            int j = 0;

            while (true)
            {
                //lecture par ligne
                line = reader.ReadLine();
                //si la ligne est vide on arrête
                if (line == null)
                {
                    break;
                }
                //on affiche la ligne
                //  Console.WriteLine(line);

                char[] array = line.ToCharArray();//convertit le string en tableau de caractère
                int i = 0;//compteur i

                Console.WriteLine("j:" + j);
                for (int cpt = 0; cpt < array.Length; cpt++)//on parcourt le tableau de caractère
                {
                    if (array[cpt] != '-')// on ignore les - dans le fichier texte
                    {
                        //ajout de la case dans _map (48 ASCII de '0')
                        _map.Add(new Map(new Vector2(i*_tCase.X,j*_tCase.Y), (int)array[cpt] - 48));
                        Console.WriteLine("Pass:" + _map.Last().getPass());
                        Console.Write("i:" + i + "-");
                        i++;
                        if (i >= _width/32)//fin de la ligne
                        {
                            j++;//on passe à la ligne suivante
                            i = 0;
                        }
                    }
                }
                Console.WriteLine("|");
            }
            //affichage dans la console pour vérification
            Console.WriteLine("FOREACH:");
            foreach (Map curr in _map)
            {
                Console.Write(curr.getPass() + "-");
                if (curr.getPosition().X >= 14 * 32)
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Index:" + _map.Last());
            Console.WriteLine("Pass:" + _map.Last().getPass());
            //  Console.ReadLine();
        }

        //méthode get

        public List<Map> getMap()
        {
            return _map;
        }
    }
}
