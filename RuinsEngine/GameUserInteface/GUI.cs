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
    public class GUI
    {
        Dictionary<string, GUIObject> gUIObjects = new Dictionary<string, GUIObject>();
        FontsWorker fontsWorker = new FontsWorker();
        public TexBox focusedTextBox;

        public GUI(RenderWindow window, string FontPath)
        {
            fontsWorker.AddFont(FontPath);
            window.TextEntered += new EventHandler<TextEventArgs>(TextBoxTextEvent);
        }

        public void CreateButton(Animation Animation, string Content, String fontName, Color color, uint CharacterSize, Button.ButtonClick func, string name)
        {
            Button button = new Button(Animation, Content, fontsWorker.GetFont(fontName), color, CharacterSize, this, name);
            button.Click += func;
            gUIObjects.Add(name, button);
        }

        public void CreateButton(Texture texture, Vector2i buttonSize, string Content, String fontName, Color color, uint CharacterSize, Button.ButtonClick func, string name)
        {
            Button button = new Button(texture, buttonSize, Content, fontsWorker.GetFont(fontName), color, CharacterSize, this, name);
            button.Click += func;
            gUIObjects.Add(name, button);
        }

        public void CreateTexBox(Animation Animation, string Content, String fontName, Color color, uint CharacterSize, string name)
        {
            TexBox texBox = new TexBox(Animation, Content, fontsWorker.GetFont(fontName), color, CharacterSize, this, name);
            gUIObjects.Add(name, texBox);
        }

        public string GetText(string TextBoxName)
        {
            if (gUIObjects.TryGetValue(TextBoxName, out var obj))
                if(obj is TexBox)
                    return (obj as TexBox).Text;
            return null;
        }

        public void ChangePos(string name, Vector2f pos)
        {
            gUIObjects[name].Pos = pos;
        }

        public void Delete(string name, Vector2f pos)
        {
            gUIObjects.Remove(name);
        }

        public void Clear()
        {
            gUIObjects.Clear();
        }

        void TextBoxTextEvent(object sender, TextEventArgs e)
        {
            if (focusedTextBox != null)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Backspace))
                {
                    focusedTextBox.DeleteLastChar();
                }
                else
                {
                    focusedTextBox.AddText(e.Unicode);
                }
            }
        }

        public void Update(RenderWindow window)
        {
            Vector2f Mpos = window.MapPixelToCoords(Mouse.GetPosition(window));
            foreach (GUIObject gUIObject in gUIObjects.Values)
            {
                gUIObject.Update(Mpos, new Vector2u(0, 0));
                gUIObject.Draw(ref window);
            }
        }
    }
}
