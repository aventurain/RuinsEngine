using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsEngine
{
    class QuadTree<T>
    {
        int PointsCount = 0;
        int Capacity;
        List<Point<T>> points = new List<Point<T>>();
        Rectangle root;
        QuadTree<T> nWest;
        QuadTree<T> nEast;
        QuadTree<T> sWest;
        QuadTree<T> sEast;
        bool devided = false;
        public QuadTree(Rectangle rectangle, int capacity)
        {
            root = rectangle;
            Capacity = capacity;
        }
        private void Subdivide()
        {
            Rectangle ne = new Rectangle(new Point<int>(root.center.X + root.halfWidth / 2, root.center.Y - root.halfHight / 2), root.halfWidth, root.halfHight);
            nEast = new QuadTree<T>(ne, Capacity);
            Rectangle nw = new Rectangle(new Point<int>(root.center.X - root.halfWidth / 2, root.center.Y - root.halfHight / 2), root.halfWidth, root.halfHight);
            nWest = new QuadTree<T>(nw, Capacity);
            Rectangle se = new Rectangle(new Point<int>(root.center.X + root.halfWidth / 2, root.center.Y + root.halfHight / 2), root.halfWidth, root.halfHight);
            sEast = new QuadTree<T>(se, Capacity);
            Rectangle sw = new Rectangle(new Point<int>(root.center.X - root.halfWidth / 2, root.center.Y + root.halfHight / 2), root.halfWidth, root.halfHight);
            sWest = new QuadTree<T>(sw, Capacity);
            devided = true;
        }

        public bool AddPoint(Point<T> point)
        {
            if (!root.IsInRectange(point.X, point.Y))
                return false;

            if (points.Count < Capacity)
            {
                points.Add(point);
            }
            else
            {
                if (!devided)
                {
                    Subdivide();
                }

                if (!nEast.AddPoint(point))
                    if (!nWest.AddPoint(point))
                        if (!sEast.AddPoint(point))
                            sWest.AddPoint(point);
            }
            PointsCount++;
            return true;
        }

        public bool AddShapeAditional(Point<T> point)
        {
            if (!root.IsInRectange(point.X, point.Y))
                return false;

            if (PointsCount < Capacity)
            {
                points.Add(point);
                PointsCount++;
            }
            else
            {
                if (!devided)
                {
                    Subdivide();

                    for (int i = 0; i < points.Count; i++)
                    {
                        if (!nEast.AddShapeAditional(points[i]))
                            if (!nWest.AddShapeAditional(points[i]))
                                if (!sEast.AddShapeAditional(points[i]))
                                    sWest.AddShapeAditional(points[i]);
                    }

                    points.Clear();
                }

                if (!nEast.AddShapeAditional(point))
                    if (!nWest.AddShapeAditional(point))
                        if (!sEast.AddShapeAditional(point))
                            sWest.AddShapeAditional(point);
            }

            return true;
        }

        public void Query(Rectangle range, List<Point<T>> found)
        {
            if (!root.Intersects(range))
            {
                return;
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (range.IsInRectange(points[i].X, points[i].Y))
                    {
                        found.Add(points[i]);
                    }
                }

                if (devided)
                {
                    nWest.Query(range, found);
                    nEast.Query(range, found);
                    sWest.Query(range, found);
                    sEast.Query(range, found);
                }
            }
        }

        public void Query(Rectangle range, List<Point<T>> found, Rectangle range2)
        {
            if (!root.Intersects(range2))
            {
                return;
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    IntRect temp = (points[i].value as GameObject).GetSpriteRect();
                    if (range.Intersects(new Rectangle(new Point<int>(points[i].X + temp.Width / 2, points[i].Y + temp.Height / 2), temp.Width, temp.Height)))
                    {
                        found.Add(points[i]);
                    }
                }

                if (devided)
                {
                    nWest.Query(range, found, range2);
                    nEast.Query(range, found, range2);
                    sWest.Query(range, found, range2);
                    sEast.Query(range, found, range2);
                }
            }
        }
    }

    class Point<T>
    {
        public T value;
        public float X, Y;
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Point(float x, float y, T value)
        {
            X = x;
            Y = y;
            this.value = value;
        }
    }
    class Rectangle
    {
        public Point<int> center;
        public float halfWidth, halfHight;
        public Rectangle(Point<int> position, float Width, float Hight)
        {
            halfWidth = Width / 2;
            halfHight = Hight / 2;
            center = position;
        }

        public bool Intersects(Rectangle rectangle)
        {
            if ((center.X - halfWidth >= rectangle.center.X + rectangle.halfWidth) || (center.X + halfWidth <= rectangle.center.X - rectangle.halfWidth))
            {
                return false;
            }
            if ((center.Y - halfHight >= rectangle.center.Y + rectangle.halfHight) || (center.Y + halfHight <= rectangle.center.Y - rectangle.halfHight))
            {
                return false;
            }
            return true;
        }

        public bool IsInRectange(float X, float Y)
        {
            if (X >= center.X - halfWidth && X <= center.X + halfWidth
                && Y >= center.Y - halfHight && Y <= center.Y + halfHight)
            {
                return true;
            }
            return false;
        }
    }
}
