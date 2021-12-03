using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RuinsEngine
{
    public class TexBox : GUIObject
    {
        public bool click = false;
        public string Text { get { return visibleText.DisplayedString; } set { visibleText.DisplayedString = value; }}

        public TexBox(Animation Animation, string Content, Font font, Color color, uint CharacterSize, GUI gUI, string name) : base(gUI, name)
        {
            animation = Animation;
            visibleText = new Text();
            visibleText.Font = font;
            visibleText.FillColor = color;
            visibleText.CharacterSize = CharacterSize;
            visibleText.DisplayedString = Content;
        }

        public void AddText(string text)
        {
            this.visibleText.DisplayedString += text;
            var bounds = this.visibleText.GetGlobalBounds();
            if (animation.sprite.TextureRect.Width < bounds.Width)
                DeleteLastChar();
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
                            gUI.focusedTextBox = this;
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

        public void DeleteLastChar()
        {
            if (visibleText.DisplayedString != "")
                visibleText.DisplayedString = visibleText.DisplayedString.Substring(0, visibleText.DisplayedString.Length - 1);
        }
    }
}
