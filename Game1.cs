using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace The_anti_Platformer_Monogame
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Sprites
        Texture2D health_05;
        Texture2D health_1;
        Texture2D antihealth_1;
        Texture2D antihealth_05;
        Texture2D armor_1;
        Texture2D armor_05;
        Texture2D antiarmor_1;
        Texture2D antiarmor_05;
        Texture2D pauseMenu;
        Texture2D resumeButton;
        Texture2D optionsButton;
        Texture2D quitButton;
        Texture2D playButton;
        Texture2D ap_logo;
        Texture2D splashText;

        //Sounds

        //Music
        Song tutorial;

        //Fonts
        SpriteFont text;

        //Vector2

        //Floats
        float scaleX;
        float scaleY;
        float timer = 2;

        //Ints
        int Width = 1920;
        int Height = 1080;
        int mAlphaValue = 255;
        int mFadeIncrement = -3;

        //Strings
        string currentLevel = "tutorial";

        //Bools
        bool paused;
        bool hasReleased;
        bool isFullscreen = true;
        bool titleScreen = true;
        bool splashFinished = false;

        //Other
        Player player;
        Map map;
        Animation animation;
        Camera camera;
        GraphicsDeviceManager graphicsManager;
        Button button;
        Matrix matrix;
        double mFadeDelay = .035;

        //Consts


        //Enemies
        //Enemy enemy1 = new Enemy();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            graphics.IsFullScreen = isFullscreen;
            graphicsManager = graphics;
        }

        protected override void Initialize()
        {
            player = new Player();
            animation = new Animation();
            map = new Map();
            button = new Button();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //setting variables
            player.spriteBatch = spriteBatch;
            Tiles.Content = Content;

            titleScreen = true;
            text = Content.Load<SpriteFont>(Content.RootDirectory + "/Fonts/font");
            splashText = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/splashtext");

            //ui
            playButton = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Pause Menu/play");
            quitButton = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Pause Menu/quit");
            ap_logo = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/the anti-Platformer Logo");
            //ui

            //sounds
            //sounds
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (titleScreen)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0f)
                {
                    mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (mFadeDelay <= 0)
                {
                    mFadeDelay = .015;

                    mAlphaValue += mFadeIncrement;

                    if (mAlphaValue <= 0)
                    {
                        splashFinished = true;
                    }
                }

                if (!splashFinished)
                {
                    return;
                }

                button.IsPressed(824, 500, playButton, graphics);

                if (button.hasBeenPressed)
                {
                    //ui
                    health_1 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/health_1");
                    health_05 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/health_05");
                    antihealth_1 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/antihealth_1");
                    antihealth_05 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/antihealth_1");
                    /*
                    armor_1 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/armor_1");
                    armor_05 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/armor_05");
                    antiarmor_1 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/antiarmor_1");
                    antiarmor_05 = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Health and Armor/antiarmor_05");
                    */
                    pauseMenu = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Pause Menu/pauseMenu");
                    resumeButton = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Pause Menu/resume");
                    optionsButton = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/UI/Pause Menu/options");
                    //ui

                    //enemies
                    //enemy1.enemySpriteUpper = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Enemies/test_upper");
                    //enemy1.enemySpriteLower = Content.Load<Texture2D>(Content.RootDirectory + "/Sprites/Enemies/test_lower");

                    map.Generate(Levels.tutorial, 64);

                    player.content = Content;
                    player.Load(Content);
                    camera = new Camera(GraphicsDevice.Viewport);
                    player.LoadMap(new Vector2(0, 0));

                    map.DiscordInitialize(currentLevel);

                    titleScreen = false;

                    if(File.Exists(Path.Combine(Content.RootDirectory + "save.txt")))
                    {
                        using (StreamReader inputFile = new StreamReader(Path.Combine(Content.RootDirectory + "save.txt")))
                        {
                            string pos = inputFile.ReadLine();
                            player.Position.X = float.Parse(pos.Substring(pos.IndexOf("X:") + 2, pos.IndexOf(" Y") - (pos.IndexOf("X:") + 2)));
                            player.Position.Y = float.Parse(pos.Substring(pos.IndexOf("Y:") + 2, pos.IndexOf("}") - (pos.IndexOf("Y:") + 2)));
                        }
                    }
                    else
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(Content.RootDirectory + "save.txt")))
                        {
                            outputFile.WriteLine(new Vector2(0, 0));
                        }
                    }

                    button.IsPressed(824, 750, quitButton, graphics);

                    if (button.hasBeenPressed)
                    {
                        Exit();
                    }
                }

                return;
            }

            //for pausing
            if (paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && hasReleased)
                {
                    paused = !paused;
                    hasReleased = false;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Escape))
                {
                    hasReleased = true;
                }

                button.IsPressed(824, 340, resumeButton, graphics);

                if (button.hasBeenPressed)
                {
                    paused = !paused;
                }

                button.IsPressed(824, 484, resumeButton, graphics);

                if (button.hasBeenPressed)
                {
                    paused = !paused;
                }

                button.IsPressed(824, 636, resumeButton, graphics);

                if (button.hasBeenPressed)
                {
                    paused = !paused;
                    titleScreen = true;
                }
                return;
            }

            /*
            bool musicPlayed = false;
            if (!musicPlayed)
            {
                musicPlayed = true;
                MediaPlayer.Play(tutorial);
                MediaPlayer.IsRepeating = true;
            }
            */

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);

                //enemy1.Collision(tile.Rectangle, map.Width, map.Height);

                camera.Update(player.Position, map.Width, map.Height, graphicsManager);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && hasReleased)
            {
                paused = !paused;
                hasReleased = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                hasReleased = true;
            }

            /*
            if (!enemy1.hasBeenKilled)
            {
                enemy1.EnemyBase(1.0f, true, false, 0f, true, 0.6f, true, 1.0f, 1.0f, new Vector2(100, 0));
            }
            */

            //saving and loading
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                using(StreamWriter outputFile = new StreamWriter(Path.Combine(Content.RootDirectory + "save.txt")))
                {
                    outputFile.WriteLine(player.Position);
                    outputFile.WriteLine(player.health);
                    outputFile.WriteLine(player.velocity);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                using(StreamReader inputFile = new StreamReader(Path.Combine(Content.RootDirectory + "save.txt")))
                {
                    string pos = inputFile.ReadLine();
                    string healthfile = inputFile.ReadLine();
                    string velocity = inputFile.ReadLine();
                    player.velocity = new Vector2(0, 0);
                    player.Position.X = float.Parse(pos.Substring(pos.IndexOf("X:") + 2, pos.IndexOf(" Y") - (pos.IndexOf("X:") + 2)));
                    player.Position.Y = float.Parse(pos.Substring(pos.IndexOf("Y:") + 2, pos.IndexOf("}") - (pos.IndexOf("Y:") + 2)));
                    player.health = float.Parse(healthfile);
                    player.velocity.X = float.Parse(velocity.Substring(velocity.IndexOf("X:") + 2, velocity.IndexOf(" Y") - (velocity.IndexOf("X:") + 2)));
                    player.velocity.Y = float.Parse(velocity.Substring(velocity.IndexOf("Y:") + 2, velocity.IndexOf("}") - (velocity.IndexOf("Y:") + 2)));
                }
            }
            //saving and loading

            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            scaleX = (float)graphics.PreferredBackBufferWidth / 1920;
            scaleY = (float)graphics.PreferredBackBufferHeight / 1080;
            matrix = Matrix.CreateScale(scaleX, scaleY, 1f);

            if (titleScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin(transformMatrix: matrix);

                if (!splashFinished)
                {
                    spriteBatch.Draw(splashText, new Vector2(636, 493), new Color(255, 255, 255, mAlphaValue));
                    spriteBatch.End();
                    return;
                }

                button.HoveringOver(824, 500, playButton, graphics);

                if (button.hoveringOver)
                {
                    spriteBatch.Draw(playButton, new Rectangle(795, 490, 326, 125), Color.White);
                }
                else
                {
                    spriteBatch.Draw(playButton, new Vector2(824, 500), Color.White);
                }

                button.HoveringOver(824, 750, quitButton, graphics);

                if (button.hoveringOver)
                {
                    spriteBatch.Draw(quitButton, new Rectangle(795, 740, 326, 125), Color.White);
                }
                else
                {
                    spriteBatch.Draw(quitButton, new Vector2(824, 750), Color.White);
                }

                spriteBatch.Draw(ap_logo, new Vector2(670, 200), Color.White);

                spriteBatch.End();

                return;
            }

            if (paused)
            {
                spriteBatch.Begin(transformMatrix: matrix);

                spriteBatch.Draw(pauseMenu, new Vector2(704, 284), Color.White);

                button.HoveringOver(824, 340, resumeButton, graphics);

                if (button.hoveringOver)
                {
                    spriteBatch.Draw(resumeButton, new Rectangle(795, 330, 326, 125), Color.White);
                }
                else
                {
                    spriteBatch.Draw(resumeButton, new Rectangle(824, 340, 272, 104), Color.White);
                }

                button.HoveringOver(824, 484, optionsButton, graphics);

                if (button.hoveringOver)
                {
                    spriteBatch.Draw(optionsButton, new Rectangle(795, 474, 326, 125), Color.White);
                }
                else
                {
                    spriteBatch.Draw(optionsButton, new Vector2(824, 484), Color.White);
                }

                button.HoveringOver(824, 636, resumeButton, graphics);

                if (button.hoveringOver)
                {
                    spriteBatch.Draw(quitButton, new Rectangle(795, 626, 326, 125), Color.White);
                }
                else
                {
                    spriteBatch.Draw(quitButton, new Vector2(824, 636), Color.White);
                }

                spriteBatch.End();
                return;
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //main game

            spriteBatch.Begin(transformMatrix: camera.Transform * matrix);

            if (player.isWalking && player.isGrounded && !player.isCrouched)
            {
                animation.Draw(spriteBatch, player.walkTexture, player.Position, 15, .125f, true, gameTime, player.isFacingRight);
            }

            if (!player.isGrounded && !player.isDiving && !player.isCrouched)
            {
                animation.Draw(spriteBatch, player.jumpTexture, new Vector2(player.Position.X - 13f, player.Position.Y + 17f), 9, .125f, false, gameTime, player.isFacingRight);
            }
            if (player.isDiving && !player.isCrouched)
            {
                animation.Draw(spriteBatch, player.diveTexture, new Vector2(player.Position.X, player.Position.Y + 22f), 1, 1f, false, gameTime, player.isFacingRight);
                animation.hasGrounded = true;
                animation.timer = 0f;
                animation.currentFrame = 0;
            }
            if (player.isCrouched)
            {
                animation.Draw(spriteBatch, player.crouchTexture, new Vector2(player.Position.X, player.Position.Y + 22f), 1, 1f, false, gameTime, player.isFacingRight);
                animation.hasGrounded = true;
                animation.timer = 0f;
                animation.currentFrame = 0;
            }
            if(player.isGrounded && !player.isWalking && !player.isCrouched && !player.isDiving)
            {
                animation.Draw(spriteBatch, player.texture, new Vector2(player.Position.X, player.Position.Y + 22f), 1, 1f, false, gameTime, player.isFacingRight);
                animation.hasGrounded = true;
                animation.timer = 0f;
                animation.currentFrame = 0;
            }

            /*
            if (!enemy1.hasBeenKilled)
            {
                spriteBatch.Draw(enemy1.enemySpriteUpper, enemy1.pos, Color.White);
            }
            if (!enemy1.hasBeenKilled)
            {
                spriteBatch.Draw(enemy1.enemySpriteLower, enemy1.pos, Color.White);
            }
            */

            spriteBatch.DrawString(text, "Use the left and right arrow keys \n             to move", new Vector2(200, 200), Color.White);
            map.Draw(spriteBatch);
            //spriteBatch.DrawString(pixelArt, "Test", new Vector2(0, 0), Color.White);
            spriteBatch.End();

            player.Draw(spriteBatch, matrix);

            //ui
            spriteBatch.Begin(transformMatrix: matrix);

            int x = 0;
            int y = 0;

            if (player.health % 2 == 0)
            {
                for (int i = 0; i < player.health / 2; i++)
                {
                    spriteBatch.Draw(health_1, new Vector2(i * 100 + 10, 10), Color.White);
                }
            }
            else
            {
                for (int i = 0; i < (player.health / 2); i++)
                {
                    spriteBatch.Draw(health_1, new Vector2(i * 100 + 10, 10), Color.White);
                    x = i;
                }
                spriteBatch.Draw(health_05, new Vector2(x * 100 + 10, 10), Color.White);
            }

            if (player.antihealth % 2 == 0)
            {
                for (int i = 0; i < player.antihealth / 2; i++)
                {
                    spriteBatch.Draw(antihealth_1, new Vector2((i + x) * 100 + 10, 10), Color.White);
                }
            }
            else
            {
                for (int i = 0; i < (player.antihealth / 2); i++)
                {
                    spriteBatch.Draw(antihealth_1, new Vector2((i + x) * 100 + 10, 10), Color.White);
                    y = i + x;
                }
                spriteBatch.Draw(antihealth_05, new Vector2(y * 100 + 10, 10), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
