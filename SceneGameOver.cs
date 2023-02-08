using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    class SceneGameOver : Scene
    {
        private KeyboardState oldKBState;
        private KeyboardState newKBState;
        private SceneGameplay sceneGameplay;
        private Vector2 posText;

        public SceneGameOver(SceneGameplay pSceneGameplay) : base()
        {
            sceneGameplay = pSceneGameplay;
        }

        public override void Load()
        {
            oldKBState = Keyboard.GetState();
            posText = new Vector2((GameState.screenWidth / 2) - (AssetsManager.MainFont.MeasureString("Tu as perdu! :(").X/2), 1);
            base.Load();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            newKBState = Keyboard.GetState();

            if(newKBState.IsKeyDown(Keys.Enter) && !oldKBState.IsKeyDown(Keys.Enter))
            {
                GameState.mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay, this);
            }

            oldKBState = newKBState;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sceneGameplay.Draw(spriteBatch);
            spriteBatch.DrawString(AssetsManager.MainFont, "Tu as perdu! :(", posText, Color.DarkBlue);

            base.Draw(spriteBatch);
        }

    }
}
