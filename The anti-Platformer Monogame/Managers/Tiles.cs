using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_anti_Platformer_Monogame
{

    class Tiles
    {

        protected Texture2D texture;

        public Rectangle Rectangle;
        public static ContentManager Content;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

    }

    class CollisionTiles : Tiles
    {

        public CollisionTiles(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Tiles/Tile" + i);
            this.Rectangle = newRectangle;
        }

    }

}

