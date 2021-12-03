using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using LinAl;

namespace RuinsEngine
{
    public class Button : GUIObject
    {
        public delegate void ButtonClick();
        public event ButtonClick Click;
        public bool click = false;

        public Button(Animation Animation, string Content, Font font, Color color, uint CharacterSize, GUI gUI, string name) : base(gUI, name)
        {
            animation = Animation;
            visibleText = new Text();
            visibleText.Font = font;
            visibleText.FillColor = color;
            visibleText.CharacterSize = CharacterSize;
            visibleText.DisplayedString = Content;
        }

        public Button(Texture texture, Vector2i buttonSize, string Content, Font font, Color color, uint CharacterSize, GUI gUI, string name) : base(gUI, name)
        {
            animation = new Animation(texture, new Vector2i(0,0), buttonSize, 2, 0);
            visibleText = new Text();
            visibleText.Font = font;
            visibleText.FillColor = color;
            visibleText.CharacterSize = CharacterSize;
            visibleText.DisplayedString = Content;
        }

        public override void Update(Vector2f MousePos, Vector2u WindowSize)
        {
            if (MousePos.X > pos.X && MousePos.X < pos.X + animation.sprite.TextureRect.Width)
            {
                if (MousePos.Y > pos.Y && MousePos.Y < pos.Y + animation.sprite.TextureRect.Height)
                {
                    if (click != Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        click = Mouse.IsButtonPressed(Mouse.Button.Left);
                        if (click)
                        {
                            animation.SetFrame(1);
                            return;
                        }
                        else
                        {
                            Click.Invoke();
                            animation.SetFrame(0);
                            return;
                        }
                    }
                    return;
                }
            }
            click = false;
            animation.SetFrame(0);
        }
    }
}
