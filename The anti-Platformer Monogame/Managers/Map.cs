using System.Collections.Generic;
using DiscordRPC;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_anti_Platformer_Monogame
{
    class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, height;
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map()
        {

        }

        public void Generate(int[,] map, int size)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if(number > -1)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));

                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                }
            }
        }

        public void UnLoadMap()
        {
            collisionTiles.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public DiscordRpcClient client;

        public void DiscordInitialize(string currentLevel)
        {
            client = new DiscordRpcClient("745845613723385876");

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Singleplayer",
                State = "Playing Level " + currentLevel,
                Assets = new Assets()
                {
                    LargeImageKey = "roo_head_large",
                    LargeImageText = "anti-Platformer"
                }
            });
        }
    }
}
