using AKingsDay.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace AKingsDay
{
    public class AKingsDay : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Matrix _scale;
        private static Game self;

        private static IState _currentState;
        private static IState _nextState;

        private KeyboardState _prevKeyState;

        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> SoundEffects;
        public static Dictionary<string, Song> Songs;
        public static Dictionary<int, string> LevelMaps;
        public static SpriteFont Arial;

        public static Random Rng = new Random();

        public AKingsDay()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            self = this;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            float scaleX = (float)_graphics.PreferredBackBufferWidth / 960;
            float scaleY = (float)_graphics.PreferredBackBufferHeight / 640;
            _scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(Songs["KingsAndDragons"]);
            MediaPlayer.IsRepeating = true;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LevelMaps = new Dictionary<int, string>();
            LevelMaps.Add(1, "Content/Tiled/Level1.tmx");
            LevelMaps.Add(2, "Content/Tiled/Level2.tmx");

            Textures = new Dictionary<string, Texture2D>();
            Textures.Add("Hero", Content.Load<Texture2D>("Sprites/Hero"));
            Textures.Add("Pig", Content.Load<Texture2D>("Sprites/Pig"));
            Textures.Add("Cannon", Content.Load<Texture2D>("Sprites/Cannon"));
            Textures.Add("Spikes", Content.Load<Texture2D>("Sprites/Spikes"));
            Textures.Add("Tiles", Content.Load<Texture2D>("Sprites/Tiles"));
            Textures.Add("LifeBar", Content.Load<Texture2D>("Sprites/UI/LifeBar"));
            Textures.Add("LifeBarHeart", Content.Load<Texture2D>("Sprites/UI/BigHeart"));
            Textures.Add("Panel", Content.Load<Texture2D>("Sprites/UI/Panel"));
            Textures.Add("Diamond", Content.Load<Texture2D>("Sprites/Diamond"));
            Textures.Add("Crown", Content.Load<Texture2D>("Sprites/Menu/Crown"));
            Textures.Add("MenuBG", Content.Load<Texture2D>("Sprites/Menu/Background"));

            SoundEffects = new Dictionary<string, SoundEffect>();
            SoundEffects.Add("Death", Content.Load<SoundEffect>("SoundEffects/Death"));
            SoundEffects.Add("Grunt", Content.Load<SoundEffect>("SoundEffects/Grunt"));
            SoundEffects.Add("Diamond", Content.Load<SoundEffect>("SoundEffects/Diamond"));
            SoundEffects.Add("Cannon", Content.Load<SoundEffect>("SoundEffects/Cannon"));


            Songs = new Dictionary<string, Song>();
            Songs.Add("KingsAndDragons", Content.Load<Song>("Songs/KingsAndDragons"));

            Arial = Content.Load<SpriteFont>("SpriteFonts/Arial");

            _currentState = new MenuState();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.F) & !_prevKeyState.IsKeyDown(Keys.F))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            _prevKeyState = keyboardState;

            _currentState.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, _scale);
            _currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void ChangeState(IState state)
        {
            _nextState = state;
        }

        public static void Quit()
        {
            self.Exit();
        }
    }
}