using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace RuinsEngine
{
    public class WorldRender
    {
        QuadTree<GameObject> quadTree;
        List<GameObject> movedObjects;

        const int qtCapacyti = 200;
        Vector2f maxSize = new Vector2f(0,0);
        public WorldRender(List<GameObject> movedObjects, List<GameObject> staticObjects, Vector2i mapSize)
        {
            Rectangle rectangle = new Rectangle(new Point<int>(mapSize.X / 2, mapSize.Y / 2), mapSize.X, mapSize.Y);
            quadTree = new QuadTree<GameObject>(rectangle, qtCapacyti);
            foreach(GameObject @object in staticObjects)
            {
                quadTree.AddPoint(new Point<GameObject>(@object.GetPos().X, @object.GetPos().Y, @object));
                if (@object.GetSpriteRect().Height > maxSize.Y) maxSize.Y = @object.GetSpriteRect().Height;
                if (@object.GetSpriteRect().Width > maxSize.X) maxSize.X = @object.GetSpriteRect().Width;
            }
            this.movedObjects = movedObjects;
        }

        View view;
        Rectangle searchtangle;
        Rectangle searchtangle2;
        List<Point<GameObject>> found;
        public void Draw(RenderWindow window)
        {
            view = window.GetView();
            searchtangle = new Rectangle(new Point<int>(view.Center.X, view.Center.Y), view.Size.X, view.Size.Y);
            searchtangle2 = new Rectangle(new Point<int>(view.Center.X, view.Center.Y), view.Size.X + maxSize.X, view.Size.Y + maxSize.Y);
            found = new List<Point<GameObject>>();

            quadTree.Query(searchtangle, found, searchtangle2);

            foreach (GameObject @object in movedObjects)
            {
                if (@object.HaveSprite())
                    {
                    IntRect objRect = @object.GetSpriteRect();
                    if (searchtangle.Intersects(new Rectangle(new Point<int>(@object.GetPos().X, @object.GetPos().Y), objRect.Width * 2, objRect.Height * 2)))
                    {
                        found.Add(new Point<GameObject>(@object.GetPos().X, @object.GetPos().Y, @object));
                    }
                }
            }

            var readyToDraw = found.OrderByDescending(p => p.value.GetAnimationLayer()).ThenBy(p => p.value.GetSpriteGroundPos());
            Console.SetCursorPosition(0,4);
            Console.WriteLine(readyToDraw.Count());
            foreach (Point<GameObject> point in readyToDraw)
            {
                point.value.Draw(ref window);
            }
        }
    }
}
