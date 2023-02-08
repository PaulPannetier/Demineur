using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public class Hero:Sprite
    {
        public int healthPoint { get; protected set; }
        public int maxSpeed { get; protected set; }
        public float ReloadTime { get;  set; }

        public Hero(Texture2D pTexture):base(pTexture)
        {
            healthPoint = 100;
            maxSpeed = 150;
            ReloadTime = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Milliseconds/1000f;
            if (ReloadTime > 0)
            {
                ReloadTime -= dt;
                if (ReloadTime < 0)
                    ReloadTime = 0;
            }

            base.Update(gameTime);
        }


    }


    public class Sprite : IActor
    {
        //Iactor
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public float vx { get; set; }
        public float vy { get; set; }
        public bool ToRemove { get; set; }


        //Sprite
        public Texture2D Texture { get; }

        public Sprite(Texture2D pTexture)
        {
            Texture = pTexture;
            Position = Vector2.Zero;
            ToRemove = false;
        }

        public void Move(float pX, float pY)
        {
            Position = new Vector2(Position.X + pX, Position.Y + pY);
        }

        public virtual void Update(GameTime pGameTime)
        {
            float dt = (float)pGameTime.ElapsedGameTime.TotalSeconds;
            Move(vx*dt, vy*dt);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
