using System;
using LinAl;
using SFML.System;
using SFML.Graphics;

namespace RuinsEngine
{
    public abstract class Shape
    {
        public GameObject gameObject;
        public Vector2i size;
        public Vector2f pos;
        public float ColideDirection;

        public Shape(Vector2f position, GameObject @object)
        {
            gameObject = @object;
            pos = position;
        }

        virtual public void Update(Vector2f pos) { }
        virtual public bool Colide(Shape shape) { return false; }
    }

    public class Square : Shape
    {
        public Square(Vector2i size, Vector2f position, GameObject @object) : base(position, @object)
        {
            this.size = size;
        }

        override public void Update(Vector2f pos)
        {
            this.pos = pos;
        }

        override public bool Colide(Shape shape)
        {

            if (shape is Square)
            {
                if ((pos.X + size.X <= shape.pos.X) || (pos.X >= shape.pos.X + shape.size.X))
                {
                    ColideDirection = 0;
                    return false;
                }
                if ((pos.Y + size.Y <= shape.pos.Y) || (pos.Y >= shape.pos.Y + shape.size.Y))
                {
                    ColideDirection = 0;
                    return false;
                }

                Vector2float vector2 = new Vector2float((pos.X + (size.X / 2)) - (shape.pos.X + (shape.size.X / 2)),
                                                           (pos.Y + (size.Y / 2)) - (shape.pos.Y + (shape.size.Y / 2)));
                ColideDirection = vector2.Angle;
                return true;
            }
            return false;
        }
    }

    class Circle
    {
        Circle()
        {

        }
    }
}
