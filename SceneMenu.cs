using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    class SceneMenu:Scene
    {
        private KeyboardState oldKBState;
        private KeyboardState newKBState;

        public SceneMenu() : base()
        {
            
        }
     
        public override void Load()
        {
            oldKBState = Keyboard.GetState();
            base.Load();
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            newKBState = Keyboard.GetState();

            if (newKBState.IsKeyDown(Keys.Enter) && !oldKBState.IsKeyDown(Keys.Enter))
            {
                GameState.mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay, this);
            }

            oldKBState = newKBState;        

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(AssetsManager.MainFont, "gg tu as gagné la partie!", new Vector2(600, 300), Color.White);
            base.Draw(spriteBatch);
        }

    }
}
