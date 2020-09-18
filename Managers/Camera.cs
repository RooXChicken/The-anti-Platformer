using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_anti_Platformer_Monogame
{
    public class Camera
    {
        public Matrix Transform;

        public Vector2 center;
        public Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOffset, int yOffset, GraphicsDeviceManager graphics)
        {
            center.X = position.X;
            center.Y = position.Y;

            Transform = Matrix.CreateTranslation(new Vector3(-center.X + 960, -center.Y + 540, 0));

        }
    }
}
