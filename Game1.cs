using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using G = Gamecodeur.GameState;

namespace Gamecodeur
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public G gameState;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameState = new G();
            G.mainGame = this;
            ChangeResolution(1600, 900);
        }

        protected override void Initialize()
        {
            gameState.ChangeScene(G.SceneType.Gameplay, this);
            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetsManager.Load(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected void ChangeResolution(int width, int height, bool IsFullScreen = false)
        {
            graphics.PreferredBackBufferWidth = G.screenWidth = width;
            graphics.PreferredBackBufferHeight = G.screenHeight = height;
            graphics.IsFullScreen = IsFullScreen;
            graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp); //pour le pixel art

            if (gameState.CurrentScene != null)
            {
                gameState.CurrentScene.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
