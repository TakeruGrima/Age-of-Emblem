using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01.Core
{
    class Cursor:GameObject
    {
        //instances de la classe
        private float _time2;//temps d'exécution pour déplacement du curseur
        private bool _deplace;//boolean qui stoque true si le curseur vient de se déplacer
        private Vector2 _posInTab;//stocke la position du curseur dans un tableau *32

        //constructeur
        public Cursor(int totalAnimationFrames, int frameWidth, int frameHeight)
            : base(totalAnimationFrames, frameWidth, frameHeight)
        {
            _frameIndex = _framesIndex.FRAME_1;
        }

        //méthodes get

        public bool getDeplace()
        {
            return _deplace;
        }

        public Vector2 getPosInTab()
        {
            return _posInTab;
        }

        //méthodes set
        public void setDeplace(bool deplace)
        {
            _deplace = deplace;
        }

        public void setPosition(int i, int j)
        {
            _position.X = i * 32 - 16; // la position s'écrit de cette manière car le curseur est plus grand que 
            //32 *32
            _position.Y = j * 32 - 16;

            //on initiale le posInTab car c'est plus simple à gérer
            _posInTab.X = i * 32;
            _posInTab.Y = j * 32;
        }

        //méthodes draw
        public void Draw(SpriteBatch spriteBatch,Unit player,Vector2 cursor)
        {
            //affiche le curseur d'une maniere differente si il se trouve sur une unité
            if (cursor.X == player.getPosInTab().X-16 && cursor.Y == player.getPosInTab().Y - 16)
            {
                this._source = new Rectangle(
                0,
                128,
                frameWidth,
                frameHeight);
                player.setOnUnit(true); //indique à player que le curseur est sur l'unité
            }
            //affiche le curseur d'une maniere standard
            else
            {
                this._source = new Rectangle(
                (int)_frameIndex * frameWidth,
                0,
                frameWidth,
                frameHeight);
                if(_deplace==true)//temporaire une autre variable doit être utilisé pour indiquer que le joueur
                //à sélectionner l'unité
                {
                    player.setOnUnit(true);
                }
                else
                {
                    player.setOnUnit(false);
                }
            }
            //affiche le curseur
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height),_source, Color.White);
        }

        //méthode déplacement
        public void Move(KeyboardState state, GameTime gameTime)
        {
            this._time2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
            {
                if (_time2 > 0.12f && _position.Y > 0)//le curseur se déplace vers le haut
                {
                    _deplace = true;
                    _position.Y -= 32;
                    _posInTab.Y -= 32;
                    _time2 = 0;
                }
            }
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))//le curseur se déplace vers la gauche
            {
                if (_time2 > 0.12f && _position.X > 0)
                {
                    _deplace = true;
                    _position.X -= 32;
                    _posInTab.X -= 32;
                    _time2 = 0;
                }
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))//le curseur se déplace vers le bas
            {
                if (_time2 > 0.12f && _position.Y < _WINH - 62 + 14)
                {
                    _deplace = true;
                    _position.Y += 32;
                    _posInTab.Y += 32;
                    _time2 = 0;
                }
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                if (_time2 > 0.12f && _position.X < _WINW - 62 + 14)//le curseur se déplace vers la droite
                {
                    _deplace = true;
                    _position.X += 32;
                    _posInTab.X += 32;
                    _time2 = 0;
                }
            }
        }
    }
}
