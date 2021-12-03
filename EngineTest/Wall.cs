using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsEngine;
using SFML.System;

namespace EngineTest
{
    class Wall: GameObject
    {
        public Wall(Animation animation, Vector2i maskSize, Vector2f pos)
        {
            this.animation = animation;
            shape = new Square(maskSize, new Vector2f(0, 0), this);
            SetPos(pos);
            animation.depth = 1;
        }

        public void ChajeSprite()
        {
            animation.SetFrame(1);
        }
    }
}
