using DungeonCrawl.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonCrawl
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        IMap map;
        Player p;
        InputState inputState;

        public static int MAPWIDTH;
        public static int MAPHEIGHT;
        public static readonly Camera camera = new Camera();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            MAPWIDTH = 100;
            MAPHEIGHT = 125;
            IMapGenStrat<DungeonMap> strat = new RandomRoomMapStrat<DungeonMap>(MAPWIDTH, MAPHEIGHT, 125, 4, 26, 1);
            map = strat.CreateMap();
            Tile temp = map.GetRandomWalkable();

            p = new Player(temp.X, temp.Y);
            font = Content.Load<SpriteFont>("ASCII");
            inputState = new InputState();
            camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;
            camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            camera.CenterOn(temp);


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
            inputState.Update();

            camera.HandleInput(inputState, null);
            p.Update(inputState);
            camera.CenterOn(p.GetPlayerTile(map));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            // spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
             null, null, null, null, camera.TranslationMatrix);

            foreach (Tile t in map.GetAllTiles())
            {
                spriteBatch.DrawString(font, t.Texture.ToString(), new Vector2(t.X * t.TileSize, t.Y * t.TileSize), t.color);
            }
            p.Draw(spriteBatch, font);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
