using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    public static class Utile
    {
        private static Random rand = new Random();
        private static Texture2D circle = GameState.mainGame.Content.Load<Texture2D>("circle");
        private static Texture2D rec = GameState.mainGame.Content.Load<Texture2D>("rectangle");
        private static Vector2 offsetCircle = new Vector2(25, 25);

        public static void setRandomSeed(int seed)
        {
            rand = new Random(seed);
        }

        public static int Rand(int a, int b)
        {
            return rand.Next(a, b + 1);
        }

        public static float DistCarre(int x1, int y1, int x2, int y2)
        {
            return ((float)Math.Pow((x2 - x1), 2) + (float)Math.Pow((y2 - y1), 2));
        }

        public static float DistCarre(Vector2 pos1, Vector2 pos2)
        {
            return ((float)Math.Pow((pos2.X - pos1.X), 2) + (float)Math.Pow((pos2.Y - pos1.Y), 2));
        }

        public static float DistCarre(Point pos1, Vector2 pos2)
        {
            return ((float)Math.Pow((pos2.X - pos1.X), 2) + (float)Math.Pow((pos2.Y - pos1.Y), 2));
        }

        public static float ToRad(float angle)
        {
            return (float)(angle * Math.PI) / 180;
        }

        public static float angle(Vector2 pos1, Vector2 pos2)
        {
            return (float)(Math.Atan2(pos1.Y - pos2.Y, pos1.X - pos2.X));
        }

        public static float angle(Point pos1, Vector2 pos2)
        {
            return (float)(Math.Atan2(pos1.Y - pos2.Y, pos1.X - pos2.X));
        }

        public static bool ContainPoint(Rectangle rec, Point point)
        {
            return rec.Contains(point);
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 positionCentre, Color pColor, float radius = 25f)
        {
            Vector2 scale = new Vector2(radius / 25f, radius / 25f);
            spriteBatch.Draw(circle, positionCentre, null, pColor, 0f, offsetCircle, scale, SpriteEffects.None, 1f); ;
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Vector2 size, Color color)
        {     
            spriteBatch.Draw(rec, position, null, color, 0f, Vector2.Zero, size, SpriteEffects.None, 1f); ;

        }

    }
}
