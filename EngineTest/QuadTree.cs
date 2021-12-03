using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using RuinsEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest
{
    /*
    class QuadTree
    {
        int PointsCount = 0;
        int Capacity;
        List<Point> points = new List<Point>();
        Rectangle root;
        QuadTree nWest;
        QuadTree nEast;
        QuadTree sWest;
        QuadTree sEast;
        bool devided = false;
        public QuadTree(Rectangle rectangle, int capacity)
        {
            root = rectangle;
            Capacity = capacity;
        }

        private void Subdivide()
        {
            Rectangle ne = new Rectangle(new Point(root.center.X + root.halfWidth / 2, root.center.Y - root.halfHight / 2), root.halfWidth, root.halfHight);
            nEast = new QuadTree(ne, Capacity);
            Rectangle nw = new Rectangle(new Point(root.center.X - root.halfWidth / 2, root.center.Y - root.halfHight / 2), root.halfWidth, root.halfHight);
            nWest = new QuadTree(nw, Capacity);
            Rectangle se = new Rectangle(new Point(root.center.X + root.halfWidth / 2, root.center.Y + root.halfHight / 2), root.halfWidth, root.halfHight);
            sEast = new QuadTree(se, Capacity);
            Rectangle sw = new Rectangle(new Point(root.center.X - root.halfWidth / 2, root.center.Y + root.halfHight / 2), root.halfWidth, root.halfHight);
            sWest = new QuadTree(sw, Capacity);
            devided = true;
        }

        public bool AddPoint(Point point)
        {
            if (!root.IsInRectange(point))
                return false;

            if (points.Count < Capacity)
            {
                points.Add(point);
                Stats.PointsCount++;
            }
            else
            {
                if (!devided)
                {
                    Stats.regionsCount += 4;
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

        public bool AddPoint2(Point point)
        {
            if (!root.IsInRectange(point))
                return false;

            if (PointsCount < Capacity)
            {
                points.Add(point);
                PointsCount++;
                Stats.PointsCount++;
            }
            else
            {
                if (!devided)
                {
                    Stats.regionsCount += 4;
                    Subdivide();

                    for (int i = 0; i < points.Count; i++)
                    {
                        if (!nEast.AddPoint2(points[i]))
                            if (!nWest.AddPoint2(points[i]))
                                if (!sEast.AddPoint2(points[i]))
                                    sWest.AddPoint2(points[i]);
                    }
                    
                    Stats.PointsCount -= points.Count;
                    points.Clear();
                }

                if (!nEast.AddPoint2(point))
                    if (!nWest.AddPoint2(point))
                        if (!sEast.AddPoint2(point))
                            sWest.AddPoint2(point);
            }

            return true;
        }

        public void Query(Rectangle range, List<Point> found)
        {
            Stats.regionSearched++;
            if (!root.Intersects(range))
            {
                return;
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    points[i].circleShape.FillColor = Color.Yellow;
                    Stats.PointsSearched++;
                    if (range.IsInRectange(points[i]))
                    {
                        points[i].circleShape.FillColor = Color.Magenta;
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

        public void Draw(ref RenderWindow window)
        {
            root.Draw(ref window);

            if(devided)
            {
                nWest.Draw(ref window);
                nEast.Draw(ref window);
                sWest.Draw(ref window);
                sEast.Draw(ref window);
            }

            foreach (Point point in points)
            {
                point.Draw(ref window);
            }
        }
    }

    static class Stats
    {
        public static int PointsCount = 0;
        public static int PointsSearched = 0;
        public static int regionSearched = 0;
        public static int regionsCount = 0;
    }

    class Point
    {
        public CircleShape circleShape = new CircleShape(1);

        public float X, Y;
        public Point(float x, float y)
        {
            X = x;
            Y = y;
            circleShape.FillColor = Color.Red;
            circleShape.Position = new Vector2f(x, y);
        }

        public void Draw(ref RenderWindow window)
        {
            window.Draw(circleShape);
        }
    }
    class Rectangle
    {
        RectangleShape line = new RectangleShape();
        RectangleShape line1 = new RectangleShape();
        RectangleShape line2 = new RectangleShape();
        RectangleShape line3 = new RectangleShape();

        public Point center;
        public float halfWidth, halfHight;
        public Rectangle(Point position, float Width, float Hight)
        {
            halfWidth = Width / 2;
            halfHight = Hight / 2;
            center = position;

            line.Position = new Vector2f(center.X - halfWidth, center.Y - halfHight);
            line.Size = new Vector2f(Width, 2);
            
            line2.Position = new Vector2f(center.X - halfWidth, center.Y + halfHight);
            line2.Size = new Vector2f(Width, 2);

            line1.Position = new Vector2f(center.X + halfWidth, center.Y - halfHight);
            line1.Size = new Vector2f(2, Hight);

            line3.Position = new Vector2f(center.X - halfWidth, center.Y - halfHight);
            line3.Size = new Vector2f(2, Hight);
        }

        public bool Intersects(Rectangle rectangle)
        {
            if (Math.Abs(center.X - rectangle.center.X) >= (halfWidth * 2 + rectangle.halfWidth * 2))
            {
                return false;
            }
            if (Math.Abs(center.Y - rectangle.center.Y) >= (halfHight * 2 + rectangle.halfHight * 2))
            {
                return false;
            }
            return true;
        }

        public bool IsInRectange(Point point)
        {
            if (point.X >= center.X - halfWidth && point.X <= center.X + halfWidth
                && point.Y >= center.Y - halfHight && point.Y <= center.Y + halfHight)
            {
                return true;
            }
            return false;
        }

        public void Draw(ref RenderWindow window)
        {
            window.Draw(line);
            window.Draw(line1);
            window.Draw(line2);
            window.Draw(line3);
        }

        public void Draw(ref RenderWindow window, Color color)
        {
            line.FillColor = color;
            line1.FillColor = color;
            line2.FillColor = color;
            line3.FillColor = color;
            window.Draw(line);
            window.Draw(line1);
            window.Draw(line2);
            window.Draw(line3);
        }

    }
    */
}
