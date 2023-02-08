using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G = Gamecodeur.GameState;

namespace Gamecodeur
{    
    class SceneGameplay : Scene
    {
        public Rectangle Screen { get; protected set; }
        public Tile[,] map { get; protected set; }
        public Vector2 offSetMap { get; protected set; }
        public int espace { get; protected set; } = 2;
        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        public int nbBombs { get; protected set; }
        public bool IsDefeat { get; protected set; } = false;
        public bool IsVictory { get; protected set; } = false;
        public float time = 0f;
        public Vector2 timePos = new Vector2(G.screenWidth - 105, -5);
        int nbFlag = 0;

        public SceneGameplay() : base()
        {
            G.gridWidth  = 35;
            G.gridHeight = 20;
            G.tileSize = new Vector2(40, 40);            
            offSetMap = new Vector2(((G.screenWidth - (G.gridWidth*G.tileSize.X) - (G.gridWidth*espace)) /2),
                ((G.screenHeight - (G.gridHeight * G.tileSize.Y) - (G.gridHeight * espace)) / 2));
            nbBombs = 109;
            G.nbTileToDemine = G.nbTileToDemineMax = G.gridWidth * G.gridHeight - nbBombs;
            Utile.setRandomSeed(System.DateTime.Now.Millisecond);
            InitMap();
        }

        public override void Load()
        {
            Screen = G.mainGame.Window.ClientBounds; //contient tout sur lecran
            oldMouseState = Mouse.GetState();            
            base.Load();
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            base.UnLoad();
        }

        public Tile[,] ResizeMap(int width, int height)
        {
            return new Tile[width,height];
        }

        public void InitMap()
        {
            map = ResizeMap(G.gridHeight, G.gridWidth);

            for (int l = 0; l < map.GetLength(0); l++)
            {
                int y = (int)(l * G.tileSize.Y + l*espace + offSetMap.Y);
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    int x = (int)(c * G.tileSize.X + c*espace + offSetMap.X);
                    map[l, c] = new Tile(new Vector2(x, y));
                }
            }
            // on ajoute les bombes
            AddBombs();
            // on regarde le nb de bombes autour de chaques cases
            for (int l = 0; l < map.GetLength(0); l++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    int compteur = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if((l + i >= 0 && l + i <= map.GetLength(0)-1) && (c + j >= 0 && c + j <= map.GetLength(1) - 1) && !(i == 0 && j == 0)) // la case exite
                            {
                                if(map[l+i, c+j].type == "bombs")
                                {
                                    compteur++;
                                }
                            }
                        }
                    }
                    map[l, c].value = compteur;

                    switch (map[l, c].value)
                    {
                        case 1 :
                            map[l, c].changeColorText(Color.White);
                            break;
                        case 2 :
                            map[l, c].changeColorText(Color.Wheat);
                            break;
                        case 3 :
                            map[l, c].changeColorText(Color.Yellow);
                            break;
                        case 4 :
                            map[l, c].changeColorText(Color.DarkGoldenrod);
                            break;
                        case 5 :
                            map[l, c].changeColorText(Color.MonoGameOrange);
                            break;
                        case 6 :
                            map[l, c].changeColorText(Color.Purple);
                            break;
                        case 7 :
                            map[l, c].changeColorText(Color.DarkViolet);
                            break;
                        case 8 :
                            map[l, c].changeColorText(Color.Black);
                            break;
                        default:
                            map[l, c].changeColorText(Color.White);
                            break;
                    }

                    if(map[l, c].type == "bombs")
                    {
                        map[l, c].value = -1;
                    }
                }
            }
        }

        public void AddBombs()
        {
            int compteur = 0;
            while (true)
            {
                int l = Utile.Rand(0, map.GetLength(0) - 1);
                int c = Utile.Rand(0, map.GetLength(1) - 1);
                if(map[l, c].type == "empty")
                {
                    map[l, c].type = "bombs";
                    compteur++;
                    if(compteur >= nbBombs)
                    {
                        break;
                    }
                }
            }
        }

        public void MouseClick(Point mousePosition)
        {            
            if ((mousePosition.X > offSetMap.X && mousePosition.X < offSetMap.X + (map.GetLength(1) * (G.tileSize.X + espace)))
                && (mousePosition.Y > offSetMap.Y && mousePosition.Y < offSetMap.Y + (map.GetLength(0) * (G.tileSize.Y + espace))))
            {

                int l = (int)Math.Floor((mousePosition.Y - offSetMap.Y) / (G.tileSize.Y + espace));
                int c = (int)Math.Floor((mousePosition.X - offSetMap.X) / (G.tileSize.X + espace));

                if (G.nbTileToDemine == G.nbTileToDemineMax)
                {
                    while (true)
                    {
                        if(map[l, c].value == 0)
                        {
                            break;
                        }
                        InitMap();
                        l = (int)Math.Floor((mousePosition.Y - offSetMap.Y) / (G.tileSize.Y + espace));
                        c = (int)Math.Floor((mousePosition.X - offSetMap.X) / (G.tileSize.X + espace));
                    }
                    time = 0f;
                }

                if (map[l,c].IsHide)
                {
                    if (!map[l, c].haveAFlag)
                    {
                        if(map[l, c].value == 0)
                        {
                            FloodFill(l, c, 0);
                        }

                        map[l, c].DiscoverTile();
                        if (map[l, c].type == "bombs")
                        {
                            IsDefeat = true;
                        }
                    }
                }
                else
                {
                    int compteur = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (TileExist(l + i, c + j) && !(i == 0 && j == 0)) // la case exite
                            {
                                if (map[l + i, c + j].haveAFlag)
                                {
                                    compteur++;
                                }
                            }
                        }
                    }
                    if(compteur == map[l, c].value || map[l, c].value == 0)
                    {
                        DiscoverAround(l, c);
                    }
                }                
            }            
        }

        public void DiscoverAround(int l, int c)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (TileExist(l + i, c + j) && !(i == 0 && j == 0)) // la case existe
                    {
                        if(!map[l + i, c + j].haveAFlag)
                        {
                            if(map[l + i, c + j].type == "bombs")
                            {
                                IsDefeat = true;
                            }
                            if(map[l + i, c + j].value == 0 && map[l + i, c + j].IsHide)
                            {
                                FloodFill(l + i, c + j, 0);
                            }
                            map[l + i, c + j].DiscoverTile();
                        }
                    }
                }
            }
        }

        public void FloodFill(int l, int c, int value)
        {
            map[l, c].DiscoverTile();

            if (TileExist(l - 1, c))// haut
            {
                if(map[l - 1, c].value == value && map[l - 1, c].IsHide)
                {
                    FloodFill(l - 1, c, value);
                }
            }
            if (TileExist(l + 1, c))// bas
            {
                if (map[l + 1, c].value == value && map[l + 1, c].IsHide)
                {
                    FloodFill(l + 1, c, value);
                }
            }
            if (TileExist(l, c + 1))// droite
            {
                if (map[l, c + 1].value == value && map[l, c + 1].IsHide)
                {
                    FloodFill(l, c + 1, value);
                }
            }
            if (TileExist(l, c - 1))// gauche
            {
                if (map[l, c - 1].value == value && map[l, c - 1].IsHide)
                {
                    FloodFill(l, c - 1, value);
                }
            }

            if (TileExist(l - 1, c + 1))// haut - droit
            {
                if (map[l - 1, c + 1].value == value && map[l - 1, c + 1].IsHide)
                {
                    FloodFill(l - 1, c + 1, value);
                }
            }
            if (TileExist(l - 1, c - 1))// haut - gauche
            {
                if (map[l - 1, c - 1].value == value && map[l - 1, c - 1].IsHide)
                {
                    FloodFill(l - 1, c - 1, value);
                }
            }
            if (TileExist(l + 1, c + 1))// bas - droit
            {
                if (map[l + 1, c + 1].value == value && map[l + 1, c + 1].IsHide)
                {
                    FloodFill(l + 1, c + 1, value);
                }
            }
            if (TileExist(l + 1, c - 1))// bas - gauche
            {
                if (map[l + 1, c - 1].value == value && map[l + 1, c - 1].IsHide)
                {
                    FloodFill(l + 1, c - 1, value);
                }
            }

            DiscoverAround(l, c);
        }

        public bool TileExist(int l, int c)
        {
            return (l >= 0 && l <= map.GetLength(0) - 1 && c >= 0 && c <= map.GetLength(1) - 1);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            newMouseState = Mouse.GetState();
            time += dt;

            if(IsDefeat)
            {
                G.mainGame.gameState.ChangeScene(G.SceneType.GameOver, this);
            }
            if(G.nbTileToDemine == 0)
            {
                IsVictory = true;
                G.mainGame.gameState.ChangeScene(G.SceneType.Menu, this);
            }

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                MouseClick(newMouseState.Position);
            }

            if((newMouseState.Position.X > offSetMap.X && newMouseState.Position.X < offSetMap.X + (map.GetLength(1) * (G.tileSize.X + espace)))
                && (newMouseState.Position.Y > offSetMap.Y && newMouseState.Position.Y < offSetMap.Y + (map.GetLength(0) * (G.tileSize.Y+ espace))))
            {
                int i = (int)Math.Floor((newMouseState.Position.Y - offSetMap.Y) / (G.tileSize.Y + espace));
                int j = (int)Math.Floor((newMouseState.Position.X - offSetMap.X) / (G.tileSize.X + espace));

                for (int l = 0; l < map.GetLength(0); l++)
                {
                    for (int c = 0; c < map.GetLength(1); c++)
                    {
                        if(!(l == i && c == j))
                        {
                            map[l, c].changeColor(map[l,c].colorHover);
                        }
                        else
                        {
                            map[l, c].changeColor(Color.ForestGreen);
                        }
                    }
                }
                if (newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                {
                    if(map[i, j].IsHide)
                    {
                        map[i, j].haveAFlag = !map[i, j].haveAFlag;
                        if(map[i, j].haveAFlag)
                        {
                            nbFlag++;
                        }
                        else
                        {
                            nbFlag--;
                        }
                    }
                }
            }
            else
            {
                for (int l = 0; l < map.GetLength(0); l++)
                {
                    for (int c = 0; c < map.GetLength(1); c++)
                    {
                        map[l, c].changeColor(map[l, c].colorHover);
                    }
                }
            }

            oldMouseState = newMouseState;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int l = 0; l < map.GetLength(0); l++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    map[l, c].Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(AssetsManager.MainFont, (nbBombs - nbFlag).ToString(), Vector2.Zero, Color.White);
            /*
             int a = (int)Math.Floor((newMouseState.Position.Y - offSetMap.Y) / (G.tileSize.Y + espace));
             int b = (int)Math.Floor((newMouseState.Position.X - offSetMap.X) / (G.tileSize.X + espace));

            spriteBatch.DrawString(AssetsManager.MainFont, map[a, b].IsHide.ToString(), new Vector2(0, 25), Color.White);
            spriteBatch.DrawString(AssetsManager.MainFont, map[a, b].type.ToString(), new Vector2(0, 45), Color.White);
            spriteBatch.DrawString(AssetsManager.MainFont, map[a, b].value.ToString(), new Vector2(0, 65), Color.White);
            spriteBatch.DrawString(AssetsManager.MainFont, map[a, b].haveAFlag.ToString(), new Vector2(0, 85), Color.White);
            */
            spriteBatch.DrawString(AssetsManager.MainFont, "Time : " + Math.Floor(time).ToString(), timePos, Color.White);

            base.Draw(spriteBatch);
        }
    }
}
