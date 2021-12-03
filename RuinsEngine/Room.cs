using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace RuinsEngine
{
    public abstract class Room
    {
        protected Game game;
        
        public List<GameObject> movebleObjects = new List<GameObject>();
        public List<GameObject> staticObjects = new List<GameObject>();
        public Vector2i mapsize;

        public Room(Game game)
        {
            this.game = game;
        }

        virtual public void Save()
        {

        }
        virtual public void Load()
        {

        }
        virtual public void Update()
        {

        }
    }
}
