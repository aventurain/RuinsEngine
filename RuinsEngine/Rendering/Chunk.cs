using SFML.System;
using SFML.Graphics;

namespace RuinsEngine
{
    public class Chunk
    {
        public Vector2i pos = new Vector2i();
        public Vector2i center = new Vector2i();
        public VertexArray vertexArray = new VertexArray(PrimitiveType.Quads, 0);
        public int WidthAndheight = 0;

        public Chunk(int X, int Y, int widthAndHeight)
        {
            pos.X = X;
            pos.Y = Y;
            center.X = X + widthAndHeight / 2;
            center.Y = Y + widthAndHeight / 2;
            WidthAndheight = widthAndHeight;
        }

        public bool IsInChunk(Tile tile)
        {
            //если зараотает нормально то поменяю на ААВВ3
            if (tile.pos.X >= pos.X && tile.pos.X + tile.WidthAndHeight <= pos.X + WidthAndheight)
            {
                if (tile.pos.Y >= pos.Y && tile.pos.Y + tile.WidthAndHeight <= pos.Y + WidthAndheight)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(ref RenderWindow window, RenderStates World)
        {
            window.Draw(vertexArray, World);
        }
    }

    public class Tile
    {
        //добавить гетеры сеттеры
        public string type;
        public int number;
        public Tile Tileup;
        public Tile TileLeft;
        public Tile TileDown;
        public Tile TileRight;
        public Vector2f pos;
        public bool isWall = false;
        public Vector2f PosOnTexture;
        public int WidthAndHeight;


        public Tile(string type, (int, int) cords, int number, bool isWall, Vector2f posOnTexture, int textureSize)
        {
            this.type = type;
            pos.X = cords.Item1;
            pos.Y = cords.Item2;
            this.number = number;
            this.isWall = isWall;
            PosOnTexture.X = posOnTexture.X;
            PosOnTexture.Y = posOnTexture.Y;
            WidthAndHeight = textureSize;
        }
    }
}
