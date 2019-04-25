using DungeonCrawl.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using DungeonCrawl.Classes.GeneticPathfinding;
using System;

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
        public static Random r = new Random();
        GenomeMutator mutator;

        Tile start;
        Tile end;

        public static int MAPWIDTH;
        public static int MAPHEIGHT;
        public static int TILEMULTIPLIER = 32;
        public static readonly Camera camera = new Camera();
        public static Dictionary<string, Texture2D> sprites;
        List<IMap> dungeon = new List<IMap>();

        List<Genome> genomes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;

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
            MAPWIDTH = 40;
            MAPHEIGHT = 40;
            sprites = TextureLoader.LoadTextures<Texture2D>(Content, "Sprites");
            IMapGenStrat<DungeonMap> strat = new RandomRoomMapStrat<DungeonMap>(MAPWIDTH, MAPHEIGHT, 30, 1, 3, 10);
            map = strat.CreateMap();
            genomes = new List<Genome>();
            mutator = new GenomeMutator();

            for (int i = 0; i < 20; i++)
            {
                genomes.Add(new Genome(8000));
            }

            foreach (Genome g in genomes)
            {
               mutator.AddGenomeWalker(new GenomeWalker(map.GetStartTile().X, map.GetStartTile().Y, map, g));
            }

            Tile temp = map.GetRandomWalkable();


            p = new Player(temp.X, temp.Y, ref map);
            font = Content.Load<SpriteFont>("ASCII");
            inputState = new InputState();
            camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;
            camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            camera.CenterOn(temp);

            start = map.GetRandomWalkable();
            end = map.GetRandomWalkable();

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

            foreach (GenomeWalker walker in mutator.GetGenomeWalkers())
            {
                walker.Update(inputState);
            }

            if (inputState.IsDown(PlayerIndex.One))
            {
                mutator.Evolve();
            }

            //camera.CenterOn(p.GetPlayerTile(map));

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
                if (t.Type == TileType.Floor)
                {
                    spriteBatch.Draw(t.texture2d, new Vector2(t.X * t.TileSize, t.Y * t.TileSize), t.color);
                }
                else if(t.Type == TileType.Wall)
                {
                    spriteBatch.Draw(t.texture2d, new Vector2(t.X * t.TileSize, t.Y * t.TileSize), t.color);
                }
                else if (t.Type == TileType.Start)
                {
                    spriteBatch.Draw(t.texture2d, new Vector2(t.X * t.TileSize, t.Y * t.TileSize), Color.Red);
                }
                else if (t.Type == TileType.End)
                {
                    spriteBatch.Draw(t.texture2d, new Vector2(t.X * t.TileSize, t.Y * t.TileSize), Color.Blue);
                }
            }

            foreach (GenomeWalker walker in mutator.GetGenomeWalkers())
            {
                walker.Draw(Content, spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
