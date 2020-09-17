using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace The_anti_Platformer_Monogame
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private Texture2D textures;
        Vector2 pos;
        Vector2 velocitySpeed;
        float angles;
        float angleVelocity;
        Color colors;
        float sizes;
        int timetl;

        public ParticleEngine(Texture2D texture, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = texture;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void Update()
        {
            int total = 10;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures;
            Vector2 position = pos;
            Vector2 velocity = velocitySpeed;
            float angle = angles;
            float angularVelocity = angleVelocity;
            Color color = colors;
            float size = sizes;
            int ttl = timetl;

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
            spriteBatch.Begin(transformMatrix: matrix);
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void SetVariables(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            texture = textures;
            pos = position;
            velocitySpeed = velocity;
            angles = angle;
            angleVelocity = angularVelocity;
            colors = color;
            sizes = size;
            timetl = ttl;
        }
    }
}
