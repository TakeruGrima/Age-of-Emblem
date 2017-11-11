using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Age_of_emblem_v1._01.Core;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Age_of_emblem_v1._01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int WIN_H = 320 * 1;
        public const int WIN_W = 480 * 1;

        World world;
        Cursor cursor;
        Unit player;
        Case deplace;
        Trajet trajet;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WIN_W;
            graphics.PreferredBackBufferHeight = WIN_H;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            world = new World("001", new Vector2(32, 32));//on charge la map 001 et les cases font 32*32
            cursor = new Cursor(4, 64, 64);//4 frames d'animation pour le curseur, le curseur fait 64*64 pixel
            player = new Unit();
            deplace = new Case();
            trajet = new Trajet(32, 32);//les fleches sont en 32*32 pixel
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            world.setTexture(Content.Load<Texture2D>("images/map"));
            world.setPosition(new Vector2(0, 0));
            world.setTaille(new Vector2(480, 320));

            cursor.setTexture(Content.Load<Texture2D>("images/Cursor"));
            cursor.setPosition(14,9);
            cursor.setTaille(new Vector2(64, 64));
            cursor.setDeplace(false);//indique que le curseur ne bouge pas

            player.setTexture(Content.Load<Texture2D>("images/Eliwood"));
            player.setPosition(14,9);
            player.setSource(2, 64, 62, 64);//on charge l'image de base
            player.setAnim(2); // Défini le sens dans lequel va aller l'animation dans l'image (vers le bas 2 et 
            //vers le haut 8 )
            player.setTaille(new Vector2(62, 64));
            player.setRayon(5);//défini le rayon de déplacement de l'unité
            player.setTMap(WIN_W, WIN_H);
            player.setMoveDef(false);// = true si le champs de déplacement de l'unité à été défini

            deplace.setTexture(Content.Load<Texture2D>("images/case_deplace"));
            deplace.setSource(new Rectangle(0, 0, 32, 32));

            trajet.setTexture(Content.Load<Texture2D>("images/fleche_move"));
            trajet.setVide(true);//le trajet est vide
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            cursor.Move(Keyboard.GetState(), gameTime);
            cursor.UpdateFrame(gameTime);
            deplace.anim(gameTime);
            player.anim(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if(player.getMoveDef()==false)//si le champs de déplacement de l'unité n'est pas défini
            {
                player.define_move(world);//on défini le champs de déplacement
            }
            spriteBatch.Begin();//l'affichage commence
            world.Draw(spriteBatch);
            deplace.DrawCase(spriteBatch, player, world);
            if(cursor.getDeplace()==true)//l'unité va bouger
            {
                trajet.addChemin(cursor.getPosInTab(), player, world);//ajout de la case dans le chemin
                trajet.Draw(spriteBatch);//affichage du chemin
            }
            player.Draw(spriteBatch);
            cursor.Draw(spriteBatch,player,cursor.getPosition());
            spriteBatch.End();//fin de l'affichage
            base.Draw(gameTime);//AFFICHE TOUT CE QUI EST DEMANDE
        }
    }
}
