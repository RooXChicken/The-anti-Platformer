using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_anti_Platformer_Monogame
{
    class Animation
    {
        public float timer;
        public int currentFrame;
        public bool hasGrounded;
        int textureWidth;

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 Position, int frameCount, float animationSpeed, bool isLooping, GameTime gameTime, bool isFacingRight)
        {
            textureWidth = texture.Width / frameCount;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > animationSpeed)
            {
                timer = 0f;
                currentFrame++;
            }

            if (isLooping && currentFrame > frameCount - 1)
            {
                currentFrame = 0;
            }
            if (!isLooping)
            {
                if(currentFrame > frameCount - 1)
                {
                    currentFrame = frameCount - 1;
                }
            }
            if (isFacingRight)
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X - 22, (int)Position.Y - 22, textureWidth, texture.Height), new Rectangle(currentFrame * textureWidth, 1, textureWidth, texture.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
            }
            else
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X - 22, (int)Position.Y - 22, textureWidth, texture.Height), new Rectangle(currentFrame * textureWidth, 1, textureWidth, texture.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 1f);
            }
        }
    }
}
