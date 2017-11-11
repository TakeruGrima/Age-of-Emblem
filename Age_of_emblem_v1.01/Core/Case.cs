using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Age_of_emblem_v1._01.Core
{
    class Case:GameObject
    {
        //Voir la classe mère GameObject pour plus d'informations
        //Cette classe est utilisé pour afficher les cases composant le champs de déplacement d'une unité

        //La fonction anim fait l'animation des cases (15 frames) montrant une difference d'éclairage sur les cases
        public void anim(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_time > 0.12f)// défini le temps que le jeu attends pour afficher la prochaine image
            {
                //Affiche la premiere image de l'animation si la derniere a été afficher(32 par défaut est la
                //taille d'un composant graphique du jeu 
                if (_source.X == 32 * 15) 
                {
                    Console.WriteLine("_source.X=" + _source.X);
                    _source.X = 0;
                }
                //Afficher l'image suivante 
                else
                {
                    _source.X += 32;
                }
                _time = 0;
            }
        }

        //DrawCase affiche les cases composant le champs de déplacement
        public void DrawCase(SpriteBatch spriteBatch, Unit player, World world)
        {
            //l'attribut _move d'Unit est une liste d'objets de la classe Map (voir la classe Unit.cs)
            //Un objet Map contient: une position (Vector2) et un boolean qui indique si l'unité peut "marcher" 
            //sur la case  (voir la classe Map.cs)
            foreach (Map curr in player.getMove())
            {
                if (curr.ifPass()==false)//si l'unité ne peut pas marcher sur la case
                {
                    _position = curr.getPosition();
                    this._source.Y = 1 * 32;// _source est un rectangle qui représente la selection dans l'image
                    //qui va être affiché
                    this._source.Width = 32;// longeur de l'image
                    this._source.Height = 32;// largeur de l'image
                    //cette fonction sert à afficher une image: _texture est l'image à afficher, 
                    //new Rectangle défini la position est la place que va prendre l'image à l'écran,
                    //et le dernier paramètre sert à gérer l'opacité d'une image, ici elle est un peu transparente
                    spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, 32, 32), _source, Color.White * 0.5f);
                    //le résultat donnera une case rouge sur l'écran
                }
                else//si l'unité peut marcher sur la case
                {
                    _position = curr.getPosition();
                    this._source.Y = 0 * 32;
                    this._source.Width = 32;
                    this._source.Height = 32;
                    spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, 32, 32), _source, Color.White * 0.5f);
                    //le résultat donnera une case bleue sur l'écran
                }
            }
        }
    }
}
