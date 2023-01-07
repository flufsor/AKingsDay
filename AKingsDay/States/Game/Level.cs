using AKingsDay.Entities;
using AKingsDay.Entities.Movables;
using AKingsDay.Entities.Movables.Enemies;
using AKingsDay.Entities.Movables.Objects;
using AKingsDay.Entities.Movables.Traps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace AKingsDay.States.Game
{
    public class Level
    {
        public static int CurrentLevel = 1;

        public static Hero Hero;
        public static List<IEntity> Entities;
        public static List<Tile> Tiles;
        public static List<ICollidable> TileCollidables;
        public static int DiamondCount;
        public static int DiamondsFound;

        internal TmxMap _map;
        internal Texture2D _tileset;
        internal int _tilesetTilesWide;
        internal int _tileWidth;
        internal int _tileHeight;
        internal Vector2 _levelStart;

        public Level(string map)
        {
            _map = new TmxMap(map);
            _tileset = AKingsDay.Textures["Tiles"];
            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;
            _tilesetTilesWide = _tileset.Width / _tileWidth;
            Entities = new List<IEntity>();
            Tiles = new List<Tile>();
            TileCollidables = new List<ICollidable>();

            DiamondCount = 0;

            LoadLevel();
            Hero = new Hero(new Vector2((int)_levelStart.X, (int)_levelStart.Y));

            Entities.Add(Hero);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].Draw(spriteBatch);
            }
            for (int i = 0; i < Entities.Count; i++)
            {
                if (Entities[i] is IVisualizeable entity)
                {
                    entity.Draw(spriteBatch);
                }
            }
        }

        public void LoadLevel()
        {
            foreach (TmxObject colObject in _map.ObjectGroups["Collisions"].Objects)
            {
                switch (colObject.Name)
                {
                    case "":
                        TileCollidables.Add(new MapCollider(new Vector2((int)colObject.X, (int)colObject.Y), new Vector2((int)colObject.Width, (int)colObject.Height)));
                        break;
                }
            }

            foreach (TmxObject colObject in _map.ObjectGroups["Entities"].Objects)
            {
                switch (colObject.Name)
                {
                    case "Start":
                        _levelStart = new Vector2((float)colObject.X, (float)colObject.Y);
                        break;
                    case "Pig":
                        Entities.Add(new Pig(new Vector2((int)colObject.X, (int)colObject.Y)));
                        break;
                    case "Diamond":
                        Entities.Add(new Diamond(new Vector2((int)colObject.X, (int)colObject.Y)));
                        DiamondCount++;
                        break;
                    case "Cannon":
                        Entities.Add(new Cannon(new Vector2((int)colObject.X, (int)colObject.Y)));
                        break;
                    case "Spikes":
                        Entities.Add(new Spikes(new Vector2((int)colObject.X, (int)colObject.Y)));
                        break;

                }
            }

            for (var i = 0; i < _map.Layers.Count; i++)
            {
                for (int j = 0; j < _map.Layers[i].Tiles.Count; j++)
                {
                    int gid = _map.Layers[i].Tiles[j].Gid;

                    int tileFrame = gid - 1;
                    int column = tileFrame % _tilesetTilesWide;
                    int row = (int)Math.Floor(tileFrame / (double)_tilesetTilesWide);
                    float x = j % _map.Width * _tileWidth;
                    float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                    Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);

                    if (gid == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Tiles.Add(new Tile(new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), tilesetRec));
                    }
                }
            }
        }

        public static void ChangeLevel()
        {
            DiamondsFound = 0;
            CurrentLevel++;

            if (CurrentLevel <= AKingsDay.LevelMaps.Count)
            {
                AKingsDay.ChangeState(new GameState(CurrentLevel));
            }
            else
            {
                AKingsDay.ChangeState(new GameOverState());
            }
        }
        public static void RemoveEntity(IEntity entity)
        {
            Entities.Remove(entity);
        }

        public static void Update(GameTime gameTime)
        {
            if (DiamondCount - DiamondsFound == 0)
            {
                ChangeLevel();
            }

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(gameTime);
            }
        }
    }
}
