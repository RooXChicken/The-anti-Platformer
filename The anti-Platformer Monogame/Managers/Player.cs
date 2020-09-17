using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_anti_Platformer_Monogame
{
    class Player
    {

        //Textures
        public Texture2D texture;
        public Texture2D walkTexture;
        public Texture2D jumpTexture;
        public Texture2D diveTexture;
        public Texture2D crouchTexture;
        Texture2D particleTextures;

        //Ints
        int randomJumpSound;

        //Bools
        public bool isFacingRight;
        public bool isGrounded;
        public bool isCrouched;
        public bool isWalking;
        public bool isDiving;
        public bool hasJumped = false;

        //Floats
        public float health = 2f;
        public float damage;
        public float antihealth;
        float jumpPressedRemembered = 0f;
        float movementSpeed;
        float crouchedSpeed;

        //Sounds
        SoundEffect jump;
        SoundEffect dive;

        //Vectors
        private Vector2 velocity;
        public Vector2 Position;

        //Misc
        KeyboardState keyState;
        public Rectangle rectangle;
        public SpriteBatch spriteBatch;
        public ContentManager content;
        Random rand;
        ParticleEngine particleEngine;

        public Player()
        {
            rand = new Random();
        }

        public void Load(ContentManager Content)
        {
            content = Content;
            texture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Roo/idle");
            walkTexture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Roo/walk");
            jumpTexture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Roo/jump");
            diveTexture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Roo/dive");
            crouchTexture = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Roo/crouch");
            dive = content.Load<SoundEffect>(content.RootDirectory + "/Sounds/Roo/Dive");

            particleTextures = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Particles/square");
            particleEngine = new ParticleEngine(particleTextures, Position);

        }

        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            Position += velocity;

            rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

            Input(gameTime);

            if(velocity.Y < 34)
            {
                velocity.Y += 0.6f;
            }
            damage = 2;

            particleEngine.EmitterLocation = Position;
            particleEngine.SetVariables(particleTextures, Position, new Vector2(-5, 0), 0f, 0f, Color.White, 0.25f, 10);
            particleEngine.Update();
        }

        private void Input(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.Right) && !isDiving)
            {
                isFacingRight = true;
                isWalking = true;
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3 * movementSpeed * crouchedSpeed;

            }else if (keyState.IsKeyDown(Keys.Left) && !isDiving)
            {
                isFacingRight = false;
                isWalking = true;
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3 * movementSpeed * crouchedSpeed;
            }
            else
            {
                //animation.AnimationStart("idle", 15, .125f, true, position, content, spritebatch, gameTime);
                isWalking = false;
                velocity.X = 0f;
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                jumpPressedRemembered = 0.1f;
            }

            jumpPressedRemembered -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (jumpPressedRemembered >= 0f && !hasJumped)
            {
                hasJumped = true;
                isGrounded = false;
                randomJumpSound = rand.Next(1,5);
                jump = content.Load<SoundEffect>(content.RootDirectory + "/Sounds/Roo/Jump" + randomJumpSound);
                jump.Play();
                Position.Y -= 0.01f;
                velocity.Y += -17;
            }

            if (keyState.IsKeyDown(Keys.R))
            {
                movementSpeed = 2.2f;
            }
            else
            {
                movementSpeed = 1.5f;
            }

            if (keyState.IsKeyDown(Keys.Down) && !isGrounded && !isDiving && !isCrouched)
            {
                velocity.Y = -10;
                isDiving = true;
                dive.Play();
            }

            if(keyState.IsKeyDown(Keys.Down) && !isDiving)
            {
                isCrouched = true;
                crouchedSpeed = .5f;
            }
            else
            {
                isCrouched = false;
                crouchedSpeed = 1f;
            }

            if (isDiving)
            {
                if (isFacingRight)
                {
                    velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3 * 3;
                }
                if (!isFacingRight)
                {
                    velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3 * 3;
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle) && velocity.Y > 0)
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                Position.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
                isGrounded = true;
                isDiving = false;
            }

            if(velocity.Y != 0f)
            {
                hasJumped = true;
                isGrounded = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                Position.X = newRectangle.X - rectangle.Width - 2;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                Position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (isCrouched)
            {
                if (rectangle.TouchBottomOf(new Rectangle((int)Position.X, (int)Position.Y, newRectangle.Width, newRectangle.Height / 2)) && velocity.Y < 1f)
                {
                    //rectangle.Y = newRectangle.Y - rectangle.Height;
                    velocity.Y = 1f;
                }
            }
            else
            {
                if (rectangle.TouchBottomOf(newRectangle) && velocity.Y < 1f)
                {
                    //rectangle.Y = newRectangle.Y - rectangle.Height;
                    velocity.Y = 1f;
                }
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

        public void LoadMap(Vector2 pos)
        {
            Position = pos;
        }

        public void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
            //particleEngine.Draw(spriteBatch, matrix);
        }
    }
}
