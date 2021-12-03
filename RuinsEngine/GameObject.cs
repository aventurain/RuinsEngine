using SFML.Graphics;
using SFML.System;

namespace RuinsEngine
{
    //абстрактный класс для описания всех объектов мира
    public abstract class GameObject
    {
        private Vector2f pos = new Vector2f(0,0);
        public Vector2f prevPos = new Vector2f(0,0);
        public Vector2i windowPos = new Vector2i(0,0);
        protected Animation animation;
        public Shape shape;
        public float speed = 0;
        public Vector2f MaskBias = new Vector2f(0, 0);
        public Vector2f animationBias = new Vector2f(0, 0);

        public void SetPos((float, float) positon)
        {
            prevPos = pos;
            pos.X = positon.Item1;
            pos.Y = positon.Item2;
            if (animation != null)
                animation.sprite.Position = new Vector2f( pos.X + animationBias.X, pos.Y + animationBias.Y);
            if (shape != null)
                UpdateMaskPos();
        }
        public void SetPos(Vector2f pos)
        {
            prevPos = this.pos;
            this.pos = pos;
            if (animation != null)
                animation.sprite.Position = new Vector2f(pos.X + animationBias.X, pos.Y + animationBias.Y);
            if (shape != null)
                UpdateMaskPos();
        }
        public Vector2f GetPos()
        {
            return pos;
        }

        public void EditPos((float, float) positon)
        {
            pos.X += positon.Item1;
            pos.Y += positon.Item2;
            animation.sprite.Position = pos;
        }

        protected void UpdateAnimationPos()
        {
            animation.sprite.Position = new Vector2f(pos.X + animationBias.X, pos.Y + animationBias.Y);
        }

        protected void UpdateAnimSpritePos(Vector2f Pos)
        {
            animation.sprite.Position = Pos;
        }

        virtual public void ColisionDetect(GameObject @object, float ColideDir)
        {

        }

        public bool HaveSprite()
        {
            if (animation == null)
                return false;
            return true;
        }

        public void UpdateMaskPos()
        {
            shape.Update(new Vector2f(pos.X + MaskBias.X, pos.Y + MaskBias.Y));
        }

        public int GetAnimationLayer()
        {
            return animation.depth;
        }

        public IntRect GetSpriteRect()
        {

            return animation.sprite.TextureRect;
        }

        public float GetSpriteGroundPos()
        {
            return animation.sprite.Position.Y + animation.sprite.TextureRect.Height;
        }

        virtual public void Update()
        {}

        public void Draw(ref RenderWindow window)
        {
            window.Draw(animation.sprite);
            windowPos = window.MapCoordsToPixel(pos);
        }
    }
}
