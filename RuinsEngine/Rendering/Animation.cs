using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace RuinsEngine
{
    public class Animation
    {
        public float Frame, speed;
        public int depth = 0;
        public Sprite sprite = new Sprite();
        public List<IntRect> frames = new List<IntRect>();

        public Animation(Texture texture, Vector2i startPoint, Vector2i spriteRect, int count, float speed)
        {
            for (int i = 0; i < count; i++)
            {
                startPoint.X = i * spriteRect.X;
                IntRect intRect = new IntRect(startPoint, spriteRect);
                frames.Add(intRect);
            }
            sprite.Texture = texture;
            sprite.TextureRect = frames[0];
            this.speed = speed;
        }

        public Animation(Texture texture, int x, int y, int w, int h, int count, float speed)
        {
            Frame = 0;
            this.speed = speed;
            for (int i = 0; i < count; i++)
                frames.Add(new IntRect(x + (i * w), y, w, h));

            sprite.Texture = texture;
            sprite.TextureRect = frames[0];
        }

        public void Update()
        {
            Frame += speed;
            int n = frames.Count;
            if (Frame >= n) 
                Frame -= n;
            if (n > 0) 
                sprite.TextureRect = frames[(int)Frame];
        }

        public void Update(int minFrame)
        {
            Frame += speed;
            int n = frames.Count;
            if (Frame >= n) 
                Frame -= n - minFrame;
            if (n > 0) sprite.TextureRect = frames[(int)Frame];
        }
        public void Update(int minFrame, int maxFrame)
        {
            Frame += speed;
            if (Frame >= maxFrame) 
                Frame -= maxFrame - minFrame;
            if (maxFrame > 0) sprite.TextureRect = frames[(int)Frame];
        }

        public void SetFrame(float frame)
        {
            if (frame >= frames.Count)
                frame = frames.Count - 1;
            if (frames.Count > 0)
            {
                sprite.TextureRect = frames[(int)frame];
                Frame = frame;
            }

        }

        public bool IsEnd()
        {
            return Frame + speed >= frames.Count;
        }
    }
}
