using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public interface IActor
    {
        Vector2 Position { get; }
        Rectangle BoundingBox { get; }
        bool ToRemove { get; set; }

        void Update(GameTime pGameTime); //on implemente pas les fonction

        void Draw( SpriteBatch pSpriteBatch);

    }
}
