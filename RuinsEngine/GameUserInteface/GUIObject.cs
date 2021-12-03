using SFML.Graphics;
using SFML.System;


namespace RuinsEngine
{
    public abstract class GUIObject
    {
        protected Vector2f pos = new Vector2f(0, 0);
        public Vector2f Pos 
        { 
            get 
            { 
                return pos; 
            } 
            set 
            {
                pos = value;
                animation.sprite.Position = pos;
                if (visibleText != null)
                {
                    visibleText.Position = pos;
                }
            } 
        }

        public Vector2i windowPos = new Vector2i(0, 0);
        protected Animation animation;
        protected Text visibleText;
        public string name;
        protected GUI gUI;

        public GUIObject (GUI gUI, string name)
        {
            this.gUI = gUI;
            this.name = name;
        }

        protected void UpdateAnimSpritePos()
        {
            animation.sprite.Position = pos;
        }
        
        protected void UpdateAnimSpritePos(Vector2f Pos)
        {
            animation.sprite.Position = Pos;
        }

        virtual public void Update(Vector2f mousePos, Vector2u WindowSize)
        { }

        public void Draw(ref RenderWindow window)
        {
            window.Draw(animation.sprite);
            if(visibleText != null)
            {
                window.Draw(visibleText);
            }
            windowPos = window.MapCoordsToPixel(pos);
        }
    }
}
