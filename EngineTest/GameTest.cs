using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using RuinsEngine;
using LinAl;

namespace EngineTest
{
    //class Game
    //{
    //    public Vector2f vievPos = new Vector2f(0, 0);
    //    public VertexArray vertexArray = new VertexArray(PrimitiveType.Quads, 0);
    //    Texture WallT = new Texture("Textures/Player.png");
    //    List<GameObject> gameObjects = new List<GameObject>();
    //    RenderStates wals;
    //    public Game(RenderWindow window)
    //    {
    //        for (int i = 0; i < 1920; i += 32)
    //        {
    //            for (int j = 0; j < 1080; j += 32)
    //            {
    //                Animation wAnimation = new Animation(WallT, new Vector2i(0, 0), new Vector2i(32, 32), 2, 1);
    //                Wall wall = new Wall(wAnimation, new Vector2i(32, 32), new Vector2f(i, j));
    //                gameObjects.Add(wall);
    //            }
    //        }
    //    }
    //    public void SomeClick()
    //    {

    //    }
    //    public void GameCycle()
    //    {

    //    }
    //    public void Update(RenderWindow window)
    //    {
    //        foreach (GameObject @object in gameObjects)
    //        {
    //            @object.Draw(ref window);
    //        }
    //    }
    //}
    /*
    class GameTreeTests
    {
        List<GameObject> gameObjects = new List<GameObject>();
        List<GameObject> gameObjectsMove = new List<GameObject>();
        Texture buttonT = new Texture("Textures/Button.png");
        GUI Gui;
        int width = 400, hight = 400;

        QuadTree quadTree;
        public GameTreeTests(RenderWindow window)
        {
            Gui = new GUI(window, "Fonts/Arial.ttf");

            Animation ButtonAnimation = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);
            Animation ButtonAnimation2 = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);
            Animation ButtonAnimation3 = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);
            Gui.CreateButton(ButtonAnimation, "Old", "Arial", Color.Black, 30, AddOld, "Кнопка1");
            Gui.CreateButton(ButtonAnimation2, "New", "Arial", Color.Black, 30, AddNew, "Кнопка2");
            Gui.CreateButton(ButtonAnimation3, "Search", "Arial", Color.Black, 30, Search, "Кнопка3");
            Gui.ChangePos("Кнопка1", new Vector2f(-100, -100));
            Gui.ChangePos("Кнопка2", new Vector2f(20, -100));
            Gui.ChangePos("Кнопка3", new Vector2f(-100, 0));

            Rectangle rectangle1 = new Rectangle(new Point(200, 200), width, hight);
            quadTree = new QuadTree(rectangle1, 4);

        }
        const int step = 3;
        public void AddOld()
        {
            Rectangle rectangle1 = new Rectangle(new Point(width / 2, hight / 2), width, hight);
            quadTree = new QuadTree(rectangle1, 40);
            Stats.PointsCount = 0;
            Stats.PointsSearched = 0;
            Stats.regionSearched = 0;
            Stats.regionsCount = 0;

            for (int i = 0; i < width; i += step)
            {
                for (int j = 0; j < hight; j += step)
                {
                    quadTree.AddPoint(new Point(i, j));
                }
            }

            searchtangle = new Rectangle(new Point(-200, -200), 100, 100);
        }

        public void AddNew()
        {
            Rectangle rectangle1 = new Rectangle(new Point(200, 200), width, hight);
            quadTree = new QuadTree(rectangle1, 40);
            Stats.PointsCount = 0;
            Stats.PointsSearched = 0;
            Stats.regionSearched = 0;
            Stats.regionsCount = 0;

            for (int i = 0; i < width; i += step)
            {
                for (int j = 0; j < hight; j += step)
                {
                    quadTree.AddPoint2(new Point(i, j));
                }
            }

            searchtangle = new Rectangle(new Point(-200, -200), 100, 100);
        }

        public void Search()
        {
            List<Point> found = new List<Point>();
            searchtangle = new Rectangle(new Point(width / 2, hight / 2), 100, 100);
            quadTree.Query(searchtangle, found);
            Console.WriteLine("Точек всего {0}; Точек проверенно {1}; Точек Найдено {2};\nРегионов всего {3}; Регионов проверенно {4};",
                Stats.PointsCount, Stats.PointsSearched, found.Count, Stats.regionsCount, Stats.regionSearched);
        }
        public void GameCycle()
        {

        }
        bool click = false;

        void AddClick(RenderWindow window)
        {
            if (click != Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                click = Mouse.IsButtonPressed(Mouse.Button.Left);
                if (click)
                {
                    return;
                }
                else
                {
                    Vector2f Mpos = window.MapPixelToCoords(Mouse.GetPosition(window));
                    if (quadTree.AddPoint(new Point(Mpos.X, Mpos.Y)))
                    {
                        Console.WriteLine("добавлена точка в {0}, {1}", Mpos.X, Mpos.Y);
                    }
                    return;
                }
            }
            click = false;
        }
        bool click2 = false;
        void SearchClick(RenderWindow window)
        {
            if (click2 != Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                click2 = Mouse.IsButtonPressed(Mouse.Button.Right);
                if (click2)
                {
                    return;
                }
                else
                {
                    List<Point> found = new List<Point>();
                    Vector2f Mpos = window.MapPixelToCoords(Mouse.GetPosition(window));
                    searchtangle = new Rectangle(new Point(Mpos.X, Mpos.Y), 100, 100);
                    quadTree.Query(searchtangle, found);
                    Console.WriteLine("Найжено {0} точек", found.Count);
                    return;
                }
            }
            click2 = false;
        }
        Rectangle searchtangle = new Rectangle(new Point(-200, -200), 100, 100);
        public void Update(RenderWindow window)
        {
            AddClick(window);
            SearchClick(window);
            quadTree.Draw(ref window);
            Gui.Update(window);
            searchtangle.Draw(ref window, Color.Green);
        }
    }
    */
    class GameTest
    {
        public Vector2f vievPos = new Vector2f(0, 0);
        List<GameObject> staticObjects = new List<GameObject>();
        public List<GameObject> movebleObjects = new List<GameObject>();
        Texture Player1T = new Texture("Textures/megaKnight.png");
        Texture Player2T = new Texture("Textures/Player2.png");
        Texture EnemyT = new Texture("Textures/Zombie.png");
        Texture WallT = new Texture("Textures/Wall.png");
        Texture GrassT = new Texture("Textures/Terrain.png");
        Texture TreeT = new Texture("Textures/Tree.png");
        ColisionsSYS colisionsSYS;
        Player player;
        WorldRender worldRender;
        public Vector2f mouseGameCords = new Vector2f();

        public GameTest(RenderWindow window)
        {
            //Animation pAnimation2 = new Animation(Player2T, new Vector2i(0, 0), new Vector2i(64, 64), 2, 1);
            player = new Player(Player1T, new Vector2i(20, 20), new Vector2f(1750, 1550), this);

            movebleObjects.Add(player);
            int counter = 0;
            
            for (int i = 4; i < 96; i++)
            {
                for (int j = 4; j < 96; j++)
                {
                    if (i % 15 == 0 && j % 15 == 0)
                    {
                        Zombie entity = new Zombie(EnemyT, new Vector2i(20, 20), new Vector2f(i * 32, j * 32));
                        movebleObjects.Add(entity);
                    }
                }
            }
            


            Console.WriteLine(counter);
            counter = 0;

            int[,] map = {{ 0, 0, 1, 0, 1, 0, 0 },
                          { 0, 0, 1, 0, 1, 0, 1 },
                          { 1, 0, 1, 1, 1, 1, 1 },
                          { 0, 0, 0, 0, 1, 0, 0 },
                          { 0, 0, 0, 0, 1, 0, 0 },
                          { 0, 0, 0, 0, 1, 0, 1 },
                          { 1, 0, 0, 0, 1, 0, 1 }};

            for (int i = 40; i < 47; i++)
            {
                for (int j = 40; j < 47; j++)
                {
                    if (map[i - 40, j - 40] == 1)
                    {
                        Animation wAnimation = new Animation(WallT, new Vector2i(0, 0), new Vector2i(32, 32), 2, 1);
                        Wall wall = new Wall(wAnimation, new Vector2i(32, 32), new Vector2f(i * 32, j * 32));
                        staticObjects.Add(wall);
                    }
                }
            }
            Animation tAnimation = new Animation(TreeT, new Vector2i(0, 0), new Vector2i(107, 124), 1, 1);
            Tree tree = new Tree(tAnimation, new Vector2i(12, 12), new Vector2f(1532, 1532));
            staticObjects.Add(tree);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if ((i < 3) || (j < 3))
                    {
                        Animation wAnimation = new Animation(WallT, new Vector2i(0, 0), new Vector2i(32, 32), 2, 1);
                        Wall wall = new Wall(wAnimation, new Vector2i(32, 32), new Vector2f(i * 32, j * 32));
                        staticObjects.Add(wall);
                    }
                    else if ((i > 97) || (j > 97))
                    {
                        Animation wAnimation = new Animation(WallT, new Vector2i(0, 0), new Vector2i(32, 32), 1, 1);
                        Wall wall = new Wall(wAnimation, new Vector2i(32, 32), new Vector2f(i * 32, j * 32));
                        staticObjects.Add(wall);
                    }
                    if (i == 30 && (j > 30) && (j < 60))
                    {
                        Animation wAnimation = new Animation(WallT, new Vector2i(0, 0), new Vector2i(32, 32), 1, 1);
                        Wall wall = new Wall(wAnimation, new Vector2i(32, 32), new Vector2f(i * 32, j * 32));
                        staticObjects.Add(wall);
                    }
                }
            }

            for (int i = 4; i < 3000; i += 32)
            {
                for (int j = 4; j < 3000; j += 32)
                {
                    Grass grass = new Grass(GrassT, new Vector2f(i, j));
                    staticObjects.Add(grass);
                }
            }

            Console.WriteLine(counter);

            colisionsSYS = new ColisionsSYS(movebleObjects, staticObjects, new Vector2i(3200, 3200), new Vector2i(220, 220));
            worldRender = new WorldRender(movebleObjects, staticObjects, new Vector2i(3200, 3200));
        }

        bool pause = false;
        public void GameCycle()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                pause = true;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.O))
            {
                pause = false;
            }
            if (!pause)
            {
                for (int i = 0; i < movebleObjects.Count; i++)
                {
                    if (movebleObjects[i] is Zombie)
                    {
                        (movebleObjects[i] as Zombie).FindPlayer(player);
                    }
                    movebleObjects[i].Update();
                }
                colisionsSYS.Update();
                vievPos = player.GetPos();
                int temp = movebleObjects.Count;
                for (int i = 0; i < temp; i++)
                {
                    if (movebleObjects[i] is PlayerHit)
                    {
                        movebleObjects.RemoveAt(i);
                        temp -= 1;
                        i -= 1;
                    }
                }
            }
        }

        public void Update(RenderWindow window)
        {
            worldRender.Draw(window);
            //player.DrawShape(window);
            //foreach (GameObject @object in movebleObjects)
            //{
            //    if (@object is PlayerHit) (@object as PlayerHit).DrawShape(window);
            //    if (@object is Zombie) (@object as Zombie).DrawShape(window);
            //}
            mouseGameCords = window.MapPixelToCoords(Mouse.GetPosition(window));
        }
    }
    class GameGUIEamle
    {
        Texture buttonT = new Texture("Textures/Button.png");
        GUI Gui;

        Vector2float dir = new Vector2float(-100, 0);
        public GameGUIEamle(RenderWindow window)
        {
            Gui = new GUI(window, "Fonts/Arial.ttf");

            Animation ButtonAnimation = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);
            Animation ButtonAnimation2 = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);
            Animation ButtonAnimation3 = new Animation(buttonT, new Vector2i(0, 0), new Vector2i(100, 50), 2, 1);

            Gui.CreateButton(ButtonAnimation, "Кнопка", "Arial", Color.Black, 30, SomeClick, "Кнопка1");
            Gui.ChangePos("Кнопка1", new Vector2f(0, 100));
            Gui.CreateTexBox(ButtonAnimation2, "", "Arial", Color.Black, 30, "Текстбокс1");
            Gui.ChangePos("Текстбокс1", new Vector2f(-200, 0));
            Gui.CreateTexBox(ButtonAnimation3, "", "Arial", Color.Black, 30, "Текстбокс2");
        }
        public void SomeClick()
        {
            Gui.ChangePos("Кнопка1", new Vector2f(dir.X, dir.Y));
            dir.RotateByDegree(90);
            Console.WriteLine(Gui.GetText("Текстбокс1"));
        }
        public void GameCycle()
        { }
        public void Update(RenderWindow window)
        {
            Gui.Update(window);
        }
    }
}
