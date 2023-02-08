using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public class GameState
    {
        public static MainGame mainGame;
        public static int screenWidth;
        public static int screenHeight;
        public static int gridWidth;
        public static int gridHeight;
        public static Vector2 tileSize;
        public static int nbTileToDemine;
        public static int nbTileToDemineMax;


        public enum SceneType
        {
            Menu,
            Gameplay,
            GameOver
        }

        public Scene CurrentScene { get; set; }

        public GameState()
        {
            
        }

        public void ChangeScene(SceneType pSceneType, object oldScene)
        {
            if (CurrentScene != null)
            {
                CurrentScene.UnLoad();
                CurrentScene = null;
            }
            switch (pSceneType)
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu();
                    break;
                case SceneType.Gameplay:
                    CurrentScene = new SceneGameplay();
                    break;
                case SceneType.GameOver:
                    CurrentScene = new SceneGameOver((SceneGameplay)oldScene);
                    break;
                default:
                    break;
            }
            CurrentScene.Load();

        }

    }
}
