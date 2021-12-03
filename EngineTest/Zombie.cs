using System;
using RuinsEngine;
using LinAl;
using SFML.System;
using SFML.Graphics;

namespace EngineTest
{
    class Zombie : GameObject
    {
        int Hp = 40;
        bool dead = false;
        int timer = 0;

        Animation walkLeft;
        Animation walkRight;
        Animation walkUp;
        Animation walkDown;

        Animation hitUp;
        Animation hitLeft;
        Animation hitDown;
        Animation hitRight;

        Animation Dead;

        Vector2float distance;
        public CircleShape circleShape1 = new CircleShape(1);
        public CircleShape circleShape2 = new CircleShape(1);
        public CircleShape circleShape3 = new CircleShape(1);
        public CircleShape circleShape4 = new CircleShape(1);

        public int maxTimer = staticRandom.random.Next(0, 100);
        public Zombie(Texture texture, Vector2i maskSize, Vector2f pos)
        {
            walkDown = new Animation(texture, new Vector2i(0, 0), new Vector2i(40, 40), 4, 0.3F);
            walkUp = new Animation(texture, new Vector2i(0, 120), new Vector2i(40, 40), 4, 0.3F);
            walkRight = new Animation(texture, new Vector2i(0, 80), new Vector2i(40, 40), 4, 0.3F);
            walkLeft = new Animation(texture, new Vector2i(0, 40), new Vector2i(40, 40), 4, 0.3F);

            Dead = new Animation(texture, new Vector2i(0, 320), new Vector2i(40, 40), 1, 0.3F);

            animation = new Animation(texture, new Vector2i(0, 0), new Vector2i(40, 40), 4, 0.1F);

            MaskBias = new Vector2f(10, 17);
            this.animation = animation;
            shape = new Square(maskSize, new Vector2f(0, 0), this);
            SetPos(pos);
        }

        public void FindPlayer(Player player)
        {
            if(shape != null)
                distance = new Vector2float(player.shape.pos.X + player.shape.size.X - (shape.pos.X + shape.size.X), 
                                        player.shape.pos.Y + player.shape.size.Y - (shape.pos.Y + shape.size.Y));
        }

        bool isDead()
        {
            if(Hp <= 0)
            {
                animation.SetFrame(0);
                animation.frames = Dead.frames;
                animation.sprite.Color = new Color(255, 255, 255, 255);
                shape = null;
                return true;
            }
            return false;
        }

        public override void Update()
        {
            animation.Update();
            if (!isDead())
            {
                if (distance.Lenght > 300)
                    speed = 100 / distance.Lenght;
                else
                    speed = 0.5F;
                Vector2float dir = distance;
                dir.Normalize();
                dir = dir * speed;
                float dirAngle = dir.Angle;
                Console.SetCursorPosition(0, 7);
                Console.WriteLine(distance.Lenght);
                if (distance.Lenght > 22)
                {
                    SetPos((GetPos().X, GetPos().Y + dir.Y));
                    SetPos((GetPos().X + dir.X, GetPos().Y));
                }
                else
                {
                    SetPos((GetPos().X, GetPos().Y));
                }

                if ((dirAngle >= 225) && (dirAngle <= 315))
                {
                    //animationBias = new Vector2f(11, 0);
                    animation.frames = walkUp.frames;
                }
                else if ((dirAngle >= 45 && dirAngle <= 135))
                {
                    //animationBias = new Vector2f(-5, 0);
                    animation.frames = walkDown.frames;
                }
                else if ((dirAngle >= 135) && (dirAngle <= 225))
                {
                    //animationBias = new Vector2f(0, 0);
                    animation.frames = walkLeft.frames;
                }
                else if ((dirAngle <= 45) || (dirAngle >= 315))
                {
                    //animationBias = new Vector2f(6, 0);
                    animation.frames = walkRight.frames;
                }
                if (GetPos() == prevPos)
                    animation.SetFrame(0);
                if (timer == 0)
                    animation.sprite.Color = new Color(255, 255, 255, 255);
                else
                    timer -= 1;
            }
        }

        public override void ColisionDetect(GameObject @object, float ColideDir)
        {
            if(@object is PlayerHit)
            {
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("ай");
                Hp -= 20;
                timer = 30;
                animation.sprite.Color = Color.Red;
                SetPos(prevPos);
                return;
            }
            float push = 0;
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
        }

        public void DrawShape(RenderWindow window)
        {
            if (shape != null)
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
}
