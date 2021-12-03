using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsEngine;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using LinAl;

namespace EngineTest
{
    class PlayerHit : GameObject
    {
        public CircleShape circleShape1 = new CircleShape(1);
        public CircleShape circleShape2 = new CircleShape(1);
        public CircleShape circleShape3 = new CircleShape(1);
        public CircleShape circleShape4 = new CircleShape(1);
        public PlayerHit(Vector2i maskSize, Vector2f pos)
        {
            shape = new Square(maskSize, pos, this);
        }
        public void DrawShape(RenderWindow window)
        {
            circleShape1.Position = new Vector2f(shape.pos.X, shape.pos.Y);
            circleShape2.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y);
            circleShape3.Position = new Vector2f(shape.pos.X, shape.pos.Y + shape.size.Y);
            circleShape4.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y + shape.size.Y);
            window.Draw(circleShape1);
            window.Draw(circleShape2);
            window.Draw(circleShape3);
            window.Draw(circleShape4);
        }

    }
}
