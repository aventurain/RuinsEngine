using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsEngine;
using SFML.System;
using SFML.Graphics;

namespace EngineTest
{
    class Grass : GameObject
    {
        public Grass(Texture grass, Vector2f pos)
        {
            animation = new Animation(grass, new Vector2i(0, 0), new Vector2i(32, 32), 3, 0F);
            animation.SetFrame(staticRandom.random.Next(0, 2));
            animation.depth = 2;
            SetPos(pos);
        }
    }
}
