using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuinsEngine;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using LinAl;

namespace EngineTest
{
    class Player : GameObject
    {
        Animation walkLeft;
        Animation walkRight;
        Animation walkUp;
        Animation walkDown;

        Animation hitUp;
        Animation hitLeft;
        Animation hitDown;
        Animation hitRight;

        bool attak = false;
        float attacAngle = 0;

        public CircleShape circleShape1 = new CircleShape(1);
        public CircleShape circleShape2 = new CircleShape(1);
        public CircleShape circleShape3 = new CircleShape(1);
        public CircleShape circleShape4 = new CircleShape(1);
        GameTest game;

        public Player(Texture texture, Vector2i maskSize, Vector2f pos, GameTest game)
        {
            this.game = game;
            walkDown = new Animation(texture, new Vector2i(0, 0), new Vector2i(58, 58), 4, 0.3F);
            walkUp = new Animation(texture, new Vector2i(0, 174), new Vector2i(58, 58), 4, 0.3F);
            walkRight = new Animation(texture, new Vector2i(0, 116), new Vector2i(58, 58), 4, 0.3F);
            walkLeft = new Animation(texture, new Vector2i(0, 58), new Vector2i(58, 58), 4, 0.3F);

            hitDown = new Animation(texture, new Vector2i(0, 232), new Vector2i(72, 72), 3, 0.3F);
            hitLeft = new Animation(texture, new Vector2i(0, 304), new Vector2i(72, 72), 3, 0.3F);
            hitRight = new Animation(texture, new Vector2i(0, 375), new Vector2i(72, 72), 3, 0.3F);
            hitUp = new Animation(texture, new Vector2i(0, 448), new Vector2i(72, 72), 3, 0.3F);

            animation = new Animation(texture, new Vector2i(0, 0), new Vector2i(58, 58), 4, 0.1F);
            MaskBias = new Vector2f(21, 37);
            shape = new Square(maskSize, new Vector2f(0, 0), this);
            animationBias = new Vector2f(-5, 0);
            SetPos(pos);
        }

        const float speed = 3;
        public override void Update()
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("X {0} Y {1}             ", GetPos().X, GetPos().Y);
            animation.Update();
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && !attak)
            {
                animationBias = new Vector2f(11, 0);
                SetPos(new Vector2f(GetPos().X, GetPos().Y - speed));
                animation.frames = walkUp.frames;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && !attak)
            {
                animationBias = new Vector2f(-5, 0);
                SetPos(new Vector2f(GetPos().X, GetPos().Y + speed));
                animation.frames = walkDown.frames;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && !attak)
            {
                animationBias = new Vector2f(0, 0);
                SetPos(new Vector2f(GetPos().X - speed, GetPos().Y));
                animation.frames = walkLeft.frames;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && !attak)
            {
                animationBias = new Vector2f(6, 0);
                SetPos(new Vector2f(GetPos().X + speed, GetPos().Y));
                animation.frames = walkRight.frames;
            }
            if (!attak && !Keyboard.IsKeyPressed(Keyboard.Key.W) && !Keyboard.IsKeyPressed(Keyboard.Key.A) && !Keyboard.IsKeyPressed(Keyboard.Key.S) && !Keyboard.IsKeyPressed(Keyboard.Key.D))
                animation.SetFrame(0);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(animation.Frame);
            //animation.SetFrame(0);

            if (Mouse.IsButtonPressed(Mouse.Button.Left) && !attak)
            {
                Vector2float vector2 = new Vector2float(game.mouseGameCords.X - GetPos().X,game.mouseGameCords.Y - GetPos().Y);
                attacAngle = vector2.Angle;
                PlayerHit hit = new PlayerHit(new Vector2i(61, 16), new Vector2f(GetPos().X, GetPos().Y - 12));
                if ((attacAngle >= 225) && (attacAngle <= 315))
                {
                    animationBias = new Vector2f(0, 0);
                    animation.frames = hitUp.frames;
                }
                else if ((attacAngle >= 45 && attacAngle <= 135))
                {
                    hit = new PlayerHit(new Vector2i(61, 16), new Vector2f(GetPos().X + 12, GetPos().Y + 69));
                    animation.frames = hitDown.frames;
                }
                else if ((attacAngle >= 135) && (attacAngle <= 225))
                {
                    hit = new PlayerHit(new Vector2i(16, 61), new Vector2f(GetPos().X - 12, GetPos().Y));
                    animationBias = new Vector2f(-10, 0);
                    animation.frames = hitLeft.frames;
                }
                else if ((attacAngle <= 45) || (attacAngle >= 315))
                {
                    hit = new PlayerHit(new Vector2i(16, 61), new Vector2f(GetPos().X + 69, GetPos().Y));
                    animationBias = new Vector2f(2, 0);
                    animation.frames = hitRight.frames;
                }
                game.movebleObjects.Add(hit);
                animation.speed = 0.08F;
                animation.SetFrame(0);
                attak = true;
                UpdateAnimationPos();
            }
            if(attak)
            {
                if(animation.IsEnd())
                {
                    if ((attacAngle >= 225) && (attacAngle <= 315))
                    {
                        animationBias = new Vector2f(11, 0);
                        animation.frames = walkUp.frames;
                    }
                    else if ((attacAngle >= 45 && attacAngle <= 135))
                    {
                        animationBias = new Vector2f(-5, 0);
                        animation.frames = walkDown.frames;
                    }
                    else if ((attacAngle >= 135) && (attacAngle <= 225))
                    {
                        animationBias = new Vector2f(0, 0);
                        animation.frames = walkLeft.frames;
                    }
                    else if ((attacAngle <= 45) || (attacAngle >= 315))
                    {
                        animationBias = new Vector2f(6, 0);
                        animation.frames = walkRight.frames;
                    }
                    UpdateAnimationPos();
                    attak = false;
                    animation.speed = 0.1F;
                }
            }

        }

        public override void ColisionDetect(GameObject @object, float ColideDir)
        {
            if(@object is PlayerHit)
            {
                return;
            }
            float push = 0;
            float push2 = 0;
            Vector2f temp = prevPos;
            if ((ColideDir > 225) && (ColideDir < 315))
            {
                push = shape.size.Y - Math.Abs(shape.pos.Y - @object.shape.pos.Y);
                SetPos(new Vector2f(GetPos().X, GetPos().Y - push));
            }
            else if ((ColideDir > 45 && ColideDir < 135))
            {
                push = shape.size.Y + @object.shape.size.Y - ((shape.pos.Y - @object.shape.pos.Y) + shape.size.Y);
                SetPos(new Vector2f(GetPos().X, GetPos().Y + push));
            }
            else if ((ColideDir > 135) && (ColideDir < 225))
            {
                push = shape.size.X + @object.shape.size.X - ((@object.shape.pos.X - shape.pos.X) + @object.shape.size.X);
                SetPos(new Vector2f(GetPos().X - push, GetPos().Y));
            }
            else if ((ColideDir < 45) || (ColideDir > 315))
            {
                push = shape.size.X + @object.shape.size.X - ((shape.pos.X - @object.shape.pos.X) + shape.size.X);
                SetPos(new Vector2f(GetPos().X + push, GetPos().Y));
            }
            /*
            if (temp == GetPos())
                animation.SetFrame(0);
            else if(ColideDir == 45)
            {
                push = shape.size.X + @object.shape.size.X - ((shape.pos.X - @object.shape.pos.X) + shape.size.X);
                push2 = shape.size.Y + @object.shape.size.Y - ((shape.pos.Y - @object.shape.pos.Y) + shape.size.Y);
                SetPos(new Vector2f(GetPos().X + push, GetPos().Y + push2));
            }
            else if (ColideDir == 135)
            {
                push = shape.size.X + @object.shape.size.X - ((@object.shape.pos.X - shape.pos.X) + @object.shape.size.X);
                push2 = shape.size.Y + @object.shape.size.Y - ((shape.pos.Y - @object.shape.pos.Y) + shape.size.Y);
                SetPos(new Vector2f(GetPos().X - push, GetPos().Y + push2));
            }
            else if (ColideDir == 225)
            {
                push = shape.size.X + @object.shape.size.X - ((@object.shape.pos.X - shape.pos.X) + @object.shape.size.X);
                push2 = shape.size.Y - Math.Abs(shape.pos.Y - @object.shape.pos.Y);
                SetPos(new Vector2f(GetPos().X - push, GetPos().Y - push2));
            }
            else if (ColideDir == 315)
            {
                push = shape.size.X + @object.shape.size.X - ((@object.shape.pos.X - shape.pos.X) + @object.shape.size.X);
                push2 = shape.size.Y - Math.Abs(shape.pos.Y - @object.shape.pos.Y);
                SetPos(new Vector2f(GetPos().X + push, GetPos().Y - push2));
            }
            */

        }
        public void DrawShape(RenderWindow window)
        {
            circleShape1.Position = new Vector2f(shape.pos.X, shape.pos.Y);
            circleShape2.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y);
            circleShape3.Position = new Vector2f(shape.pos.X, shape.pos.Y + shape.size.Y);
            circleShape4.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y + shape.size.Y);
            window.Draw(circleShape1);
            window.Draw(circleShape2);
            window.Draw(circleShape3);
            window.Draw(circleShape4);
        }
    }
}
