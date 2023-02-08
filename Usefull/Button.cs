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
    public delegate void OnClick(Button sender);

    public class Button:Sprite
    {
        public bool IsHover { get; private set; }
        private MouseState oldMouseState;
        private MouseState newMouseState;
        public OnClick onClick { get; set; } // une reference de fonction

        public Button(Texture2D texture):base(texture)
        {

        }

        public override void Update(GameTime pGameTime)
        {
            newMouseState = Mouse.GetState();
            Point MousePosition = newMouseState.Position;
            if (BoundingBox.Contains(MousePosition))
            {
                if (!IsHover)
                {
                    IsHover = true;

                }
            }
            else
            {
                IsHover = false;
            }

            if (IsHover)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    // on vient  de cliquer dedans!
                    if (onClick != null)
                    {
                        onClick(this);
                    }
                }
            }
            oldMouseState = newMouseState;

            base.Update(pGameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}
