using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public class Tile
    {
        public string type { get; set; } = "empty";
        public bool IsHide { get; protected set; } = true;
        public Vector2 position { get; protected set; }
        public Vector2 positionText { get; protected set; }
        public int value { get; set; } = 0;
        public Color color { get; protected set; } = Color.Aquamarine;
        public Color colorText { get; protected set; }
        public Color colorHover { get; protected set; } = Color.Aquamarine;
        public bool haveAFlag { get; set; } = false;
        private Vector2 flapPosition;

        public Tile(Vector2 pos)
        {
            position = flapPosition = pos;
            positionText = new Vector2(position.X + ((GameState.tileSize.X/2)*0.75f), position.Y + ((GameState.tileSize.Y / 2) * 0.1f));
        }       

        public void DiscoverTile()
        {
            if(IsHide && type == "empty")
            {
                GameState.nbTileToDemine--;
            }
            IsHide = false;            
        }

        public void changeColor(Color newColor)
        {
            this.color = newColor;
        }
        public void changeColorText(Color newColor)
        {
            this.colorText = newColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsHide)
            {
                Utile.DrawRectangle(spriteBatch, position, GameState.tileSize, color);

                if (haveAFlag)
                {
                    spriteBatch.Draw(AssetsManager.Images["flag"], flapPosition, Color.White);
                }
            }
            else
            {
                Utile.DrawRectangle(spriteBatch, position, GameState.tileSize, Color.DarkGray);
                if(type == "empty" && value != 0)
                {
                    spriteBatch.DrawString(AssetsManager.MainFont, value.ToString(), positionText, colorText);
                }
            }
        }
    }
}
