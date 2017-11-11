using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Age_of_emblem_v1._01.Core
{
    class GameObject
    {
        //instances de la classe
        protected Vector2 _position;//stoque la position de l'objet
        protected Texture2D _texture;//stoque la texture de l'objet(image)

        protected int _height;//stoque la hauteur de l'image
        protected int _width;//stoque la largeur de l'image

        protected Rectangle _source;//pour selectionner une certaine partie de l'image

        protected const int _WINH = 320;//hauteur de l'écran
        protected const int _WINW = 480;//largeur de l'écran

        protected float _time;//temps d'execution
        protected float _frameTime = 0.15f;//décalage temporelle entre 2 frames

        public _framesIndex _frameIndex;

        public enum _framesIndex
        {
            FRAME_1 = 0,
            FRAME_2 = 1,
            FRAME_3 = 2,
            FRAME_4 = 3
        }

        private int _totalFrames;
        public int totalFrames
        {
            get { return _totalFrames; }
        }
        private int _frameWidth;
        public int frameWidth
        {
            get { return _frameWidth; }
        }
        private int _frameHeight;
        public int frameHeight
        {
            get { return _frameHeight; }
        }

        //Constructeur par défaut
        public GameObject()
        {
        }
        //constructeur pour animation
        public GameObject(int totalAnimationFrames, int frameWidth, int frameHeight)
        {
            _totalFrames = totalAnimationFrames;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
        }

        //méthode set
        public void setPosition(Vector2 position)
        {
            _position = position;
        }

        public void setTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public void setTaille(Vector2 taille)
        {
            _height = (int)taille.Y;
            _width = (int)taille.X;
        }

        public void setSource(Rectangle source)
        {
            _source = source;
        }

        public void setSource(int x,int y,int width,int height)
        {
            _source = new Rectangle(x, y, width, height);
        }

        //les méthodes get
        //méthode set
        public Vector2 getPosition()
        {
            return _position;
        }

        public int getWidth()
        {
            return _width;
        }

        public int getHeight()
        {
            return _height;
        }

        //Méthodes

        //Draw image
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
        }

        //Animation
        public void UpdateFrame(GameTime gameTime)
        {
            this._time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (_time > _frameTime)
            {
                _frameIndex++;
                _time = 0f;
            }
            if ((int)_frameIndex > _totalFrames)
            {
                _frameIndex = 0;
            }
            this._source = new Rectangle(
                (int)_frameIndex * frameWidth,
                0,
                frameWidth,
                frameHeight);
        }
    }
}
