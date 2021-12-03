using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsEngine;
using SFML.System;

namespace EngineTest
{
    class Tree : GameObject
    {
        public Tree(Animation animation, Vector2i maskSize, Vector2f pos)
        {
            this.animation = animation;
            shape = new Square(maskSize, new Vector2f(0, 0), this);
            MaskBias = new Vector2f(48, 109);
            SetPos(pos);
            animation.depth = 0;
        }
    }
}
