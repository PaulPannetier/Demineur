using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public static class AssetsManager
    {
        public static SpriteFont MainFont { get; private set; }
        public static Dictionary<string, Texture2D> Images = new Dictionary<string, Texture2D>();

        public static void Load(ContentManager content) //seul les methode static peuvent acceder au objet static
        {
            MainFont = content.Load<SpriteFont>("File");
            Images["flag"] = content.Load<Texture2D>("flag");
        }

    }
}
