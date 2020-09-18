using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_anti_Platformer_Monogame
{
    class Button
    {
        bool isInputPressed = false;
        public bool hasBeenPressed;
        public bool hoveringOver;

        public void IsPressed(int posX, int posY, Texture2D texture, GraphicsDeviceManager graphics)
        {
            var scaleX = (float)graphics.PreferredBackBufferWidth / 1920;
            var scaleY = (float)graphics.PreferredBackBufferHeight / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 0f);

            var mouseState = Mouse.GetState();
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var scaledMousePosition = Vector2.Transform(mousePosition, Matrix.Invert(matrix));
            isInputPressed = mouseState.LeftButton == ButtonState.Pressed;

            var buttonRectangle = new Rectangle(posX, posY, texture.Width, texture.Height);

            if (isInputPressed && buttonRectangle.Contains((int)mousePosition.X, (int)mousePosition.Y))
            {
                hasBeenPressed = true;
            }
            else
            {
                hasBeenPressed = false;
            }
        }
        public void HoveringOver(int posX, int posY, Texture2D texture, GraphicsDeviceManager graphics)
        {
            var scaleX = (float)graphics.PreferredBackBufferWidth / 1920;
            var scaleY = (float)graphics.PreferredBackBufferHeight / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 0f);

            var mouseState = Mouse.GetState();
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var scaledMousePosition = Vector2.Transform(mousePosition, Matrix.Invert(matrix));

            var buttonRectangle = new Rectangle(posX, posY, texture.Width, texture.Height);

            if (buttonRectangle.Contains((int)mousePosition.X, (int)mousePosition.Y))
            {
                hoveringOver = true;
            }
            else
            {
                hoveringOver = false;
            }
        }
    }
}
