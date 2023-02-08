using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    abstract public class Scene
    {
        protected List<IActor> listActor;

        public Scene()
        {
            listActor = new List<IActor>();
        }

        public void Clean()
        {
            // on suppr tous les acteurs tq leur attribut ToRemove est à true
            listActor.RemoveAll(item => item.ToRemove == true);
        }

        public virtual void Load()
        { 
        
        }
        public virtual void UnLoad()
        {

        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor actor in listActor)
            {
                actor.Update(gameTime);            
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (IActor actor in listActor)
            {
                actor.Draw(spriteBatch);
            }

        }

    }
}
