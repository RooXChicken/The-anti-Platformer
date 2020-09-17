using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_anti_Platformer_Monogame
{
    class Enemy
    {
        public Vector2 pos;
        bool isFacingRight;
        public bool hasBeenKilled = false;
        public bool hasBeenActivated = false;
        bool hasStarted = false;

        Player player = new Player();
        float gravity;
        public Texture2D enemySpriteUpper;
        public Texture2D enemySpriteLower;
        Rectangle rectangle;
        Vector2 velocity;
        public void EnemyBase(float movementSpeed, bool canMove, bool followPlayer, float followRange, bool gravityEnabled, float gravitySpeed, bool walksOffEdge, float damage, float health, Vector2 startingPos)
        {
            if (!hasStarted)
            {
                hasStarted = true;
                pos = startingPos;
            }

            pos += velocity;

            if (canMove)
            {
                if (isFacingRight)
                {
                    velocity.X = movementSpeed;
                }
                else
                {
                    velocity.X = -movementSpeed;
                }
            }

            rectangle = new Rectangle((int)pos.X, (int)pos.Y, enemySpriteLower.Width + enemySpriteUpper.Width, enemySpriteUpper.Height + enemySpriteLower.Height);

            /*
            if (followPlayer)
            {
                if ()
                {

                }
            }
            */

            if (velocity.Y < 34)
            {
                velocity.Y += gravitySpeed;
            }

            if (!walksOffEdge)
            {

            }

            if(new Rectangle((int)pos.X, (int)pos.Y + 10, enemySpriteLower.Width, enemySpriteLower.Height).Contains(player.Position))
            {
                player.health -= damage;
            }

            if (new Rectangle((int)pos.X, (int)pos.Y - 5, enemySpriteUpper.Width, enemySpriteUpper.Height).Contains(player.Position.X, player.Position.Y))
            {
                health -= player.damage;
            }

            if(damage <= 0)
            {
                hasBeenKilled = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle) && velocity.Y > 0)
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                pos.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                isFacingRight = !isFacingRight;
                pos.X = newRectangle.X - rectangle.Width - 10;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                isFacingRight = !isFacingRight;
                pos.X = newRectangle.X + newRectangle.Width + 10;
            }
            
            if (rectangle.TouchBottomOf(newRectangle) && velocity.Y < 1f)
            {
                //rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 1f;
            }


            /*
            if(position.X < 0)
            {
                position.X = 0;
            }
            if(position.X > xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if(position.Y < 0)
            {
                velocity.Y = 0f;
            }
            if(position.Y > yOffset - rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }
            */
        }
    }
}
