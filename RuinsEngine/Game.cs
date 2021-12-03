using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace RuinsEngine
{
    public class Game
    {
        Room CurRoom;
        public RenderWindow window;
        public bool clouse = false;
        public View view;

        ColisionsSYS colisionsSYS;
        WorldRender worldRender;

        public Game(RenderWindow window)
        {
            this.window = window;
        }

        public Game(VideoMode videom, string name)
        {
            window = new RenderWindow(videom, name);
            window.SetFramerateLimit(60);
            window.SetActive();
            window.Closed += new EventHandler(OnClose);

            view = new View(new Vector2f(0, 0), new Vector2f(1024, 720));

            window.Resized += (sender, args2) =>
            { 
                view = new View(new Vector2f(0, 0), new Vector2f(args2.Width, args2.Height));
            };
        }

        public void ChangeRoom(Room room)
        {
            colisionsSYS = new ColisionsSYS(room.movebleObjects, room.staticObjects, room.mapsize, room.mapsize);
            worldRender = new WorldRender(room.movebleObjects, room.staticObjects, room.mapsize);
            CurRoom = room;
        }

        public void Start()
        {
            while (window.IsOpen && !clouse)
            {
                window.SetView(view);
                window.DispatchEvents();

                window.Clear(Color.Black);
                colisionsSYS.Update();
                worldRender.Draw(window);
                CurRoom.Update();
                window.Display();
            }
            window.Close();
        }

        static void OnClose(object sender, EventArgs e)
        {
            //Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
    }
}
