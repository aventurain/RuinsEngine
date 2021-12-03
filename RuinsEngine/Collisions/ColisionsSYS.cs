using System;
using LinAl;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace RuinsEngine
{
    //работа с столкновениями
    //нужно дописать оптимизациию проверки столкновений что бы проверялись не все объекты,
    //а только те что находятся в определеном радиусе от сущности
    //Sweep And Prune or QadTree


    // Короче принцеп работы этого класса
    /*
     * В классе игры создаются сущности и потом целиком передаются в данный класс
     * или
     *  ------- оставлю тут как идею: в каждый объект необходимо передать ссылку на данный класс и объект сам себя добавит
     * 
     * Из них строится quad tree
     * у этого класса бдует функция с возможностью узнать произошло ли столкновение для конкретного объекта
     */

    public class ColisionsSYS
    {
        const int qtCapacyti = 10;
        QuadTree<Shape> quadTree;
        //List<Shape> movebleObjects = new List<Shape>();
        Rectangle searchRectangle;
        List<GameObject> mObjects = new List<GameObject>();
        public ColisionsSYS(List<GameObject> movebleObjects, List<GameObject> staticObjects, Vector2i mapSize, Vector2i SizeOfChekRectangle)
        {
            mObjects = movebleObjects;
            Rectangle rectangle = new Rectangle(new Point<int>(mapSize.X / 2, mapSize.Y / 2), mapSize.X, mapSize.Y);
            quadTree = new QuadTree<Shape>(rectangle, qtCapacyti);
            foreach (GameObject @object in staticObjects)
            {
                if(@object.shape != null)
                    quadTree.AddShapeAditional(new Point<Shape>(@object.shape.pos.X, @object.shape.pos.Y, @object.shape));
            }
            searchRectangle = new Rectangle(new Point<int>(0, 0), SizeOfChekRectangle.X, SizeOfChekRectangle.Y);
        }
        public ColisionsSYS(List<GameObject> movebleObjects, List<GameObject> staticObjects, Vector2i mapSize)
        {
            mObjects = movebleObjects;
            Rectangle rectangle = new Rectangle(new Point<int>(mapSize.X / 2, mapSize.Y / 2), mapSize.X, mapSize.Y);
            quadTree = new QuadTree<Shape>(rectangle, qtCapacyti);
            Vector2i SizeOfChekRectangle = new Vector2i(0,0);
            foreach (GameObject @object in staticObjects)
            {
                if (@object.shape != null)
                    quadTree.AddShapeAditional(new Point<Shape>(@object.shape.pos.X, @object.shape.pos.Y, @object.shape));
                if (@object.shape.size.X > SizeOfChekRectangle.X)
                    SizeOfChekRectangle.X = @object.shape.size.X;
                if (@object.shape.size.Y > SizeOfChekRectangle.Y)
                    SizeOfChekRectangle.Y = @object.shape.size.Y;
            }
            foreach(GameObject @object in movebleObjects)
            {
                if (@object.shape.size.X > SizeOfChekRectangle.X)
                    SizeOfChekRectangle.X = @object.shape.size.X;
                if (@object.shape.size.Y > SizeOfChekRectangle.Y)
                    SizeOfChekRectangle.Y = @object.shape.size.Y;
            }
            searchRectangle = new Rectangle(new Point<int>(0, 0), SizeOfChekRectangle.X + (SizeOfChekRectangle.X / 2), SizeOfChekRectangle.Y + (SizeOfChekRectangle.Y / 2));
        }

        public void Update()
        {
            List<Point<Shape>> found = new List<Point<Shape>>();
            LinkedList<Shape> shapes = new LinkedList<Shape>();
            foreach (GameObject gobject in mObjects)
            {
                if (gobject.shape != null)
                {
                    found.Clear();
                    searchRectangle.center.X = gobject.shape.pos.X;
                    searchRectangle.center.Y = gobject.shape.pos.Y;
                    quadTree.Query(searchRectangle, found);
                    shapes = new LinkedList<Shape>();
                    foreach (Point<Shape> finding in found)
                    {
                        // проверять на большее перекрытие
                        if (gobject.shape.Colide(finding.value))
                        {
                            finding.value.ColideDirection = gobject.shape.ColideDirection;
                            if ((gobject.shape.ColideDirection != 225) && (gobject.shape.ColideDirection != 315) &&
                                (gobject.shape.ColideDirection != 45) && (gobject.shape.ColideDirection != 135))
                            {
                                shapes.AddFirst(finding.value);
                            }
                            else
                            {
                                shapes.AddLast(finding.value);
                            }
                        }
                    }
                    foreach (Shape finding in shapes)
                    {
                        if (gobject.shape.Colide(finding))
                            gobject.ColisionDetect(finding.gameObject, finding.ColideDirection);
                    }
                }
            }

            // баг фантомное толкание для устранения необходимо понимать кто кого толкает
            foreach (GameObject gobject1 in mObjects)
            {
                foreach (GameObject gobject2 in mObjects)
                {
                    if (gobject1.shape != null && gobject2.shape != null)
                        if (gobject1 != gobject2 && gobject1.shape.Colide(gobject2.shape))
                    {
                        gobject1.shape.gameObject.ColisionDetect(gobject2, gobject1.shape.ColideDirection);
                    }
                }
            }
        }

        //public void DebugDraw(RenderWindow window)
        //{
        //    //foreach (GameObject shape in mObjectss)
        //    //{
        //    //    shape.circleShape1.Position = new Vector2f(shape.pos.X, shape.pos.Y);
        //    //    shape.circleShape2.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y);
        //    //    shape.circleShape3.Position = new Vector2f(shape.pos.X, shape.pos.Y + shape.size.Y);
        //    //    shape.circleShape4.Position = new Vector2f(shape.pos.X + shape.size.X, shape.pos.Y + shape.size.Y);
        //    //    window.Draw(shape.circleShape1);
        //    //    window.Draw(shape.circleShape2);
        //    //    window.Draw(shape.circleShape3);
        //    //    window.Draw(shape.circleShape4);
        //    //}
        //    //List<Shape> found = new List<Shape>();
        //    //foreach (Shape shape in movebleObjects)
        //    //{
        //    //    found.Clear();
        //    //    searchRectangle.center.X = shape.pos.X;
        //    //    searchRectangle.center.Y = shape.pos.Y;
        //    //    quadTree.Query(searchRectangle, found);
        //    //    foreach (Shape finding in found)
        //    //    {
        //    //        finding.circleShape1.Position = new Vector2f(finding.pos.X, finding.pos.Y);
        //    //        finding.circleShape2.Position = new Vector2f(finding.pos.X + finding.size.X, finding.pos.Y);
        //    //        finding.circleShape3.Position = new Vector2f(finding.pos.X, finding.pos.Y + finding.size.Y);
        //    //        finding.circleShape4.Position = new Vector2f(finding.pos.X + finding.size.X, finding.pos.Y + finding.size.Y);
        //    //        window.Draw(finding.circleShape1);
        //    //        window.Draw(finding.circleShape2);
        //    //        window.Draw(finding.circleShape3);
        //    //        window.Draw(finding.circleShape4);
        //    //    }
        //    //}
        //}
    }
}
