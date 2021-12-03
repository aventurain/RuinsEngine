using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;


namespace RuinsEngine
{
    /*
    public class TextureWorker
    {
        //хочу перенести сюда всю работу с текстурами название не совсем корректно
        public Texture playerTexture = new Texture("Textures/hpd ch.png");
        public Texture World1Texture = new Texture("Textures/World1.png");

        public RenderStates world;

        /*
         * добавить послоевую обработку
         * 0 - самый низ - размытый фон
         * 1 - земля и тень от деревьев
         * 2 - слой энтитей и построек
         * 3 - слой верних объектов кроны своды и тп
         */
    /*
        public VertexArray vertexArraylayer1 = new VertexArray(PrimitiveType.Quads, 6);

        public List<Chunk> Chunks1 = new List<Chunk>();//земля и тени
        const int ChunkWidthAndHeight = 50 * 32;//в пикселях
        List<Object> DynamikLayer = new List<Object>();//динамический слой с объектами

        
        public void MakeChunks(LevelGenerator level)
        {
            world = new RenderStates(World1Texture);

            //генерация чанков
            for (int i = 0; i <= level.levelSize.X; i += ChunkWidthAndHeight)
            {
                for (int j = 0; j <= level.levelSize.Y; j += ChunkWidthAndHeight)
                {
                    Chunk chunk = new Chunk(i, j, ChunkWidthAndHeight);
                    Chunks1.Add(chunk);
                }
            }

            //заполнение их вертексами
            foreach (Chunk chunk in Chunks1)
            {
                foreach (Tile tile in level.tiles)
                {
                    //мей би заменю на более эффективную проверку
                    if (chunk.IsInChunk(tile))
                    {
                        AddQuadVertex(tile.pos, tile.PosOnTexture, tile.WidthAndHeight, chunk);
                    }
                    if (tile.pos.X > chunk.pos.X + chunk.WidthAndheight && tile.pos.Y > chunk.pos.Y + chunk.WidthAndheight) break;//ускоряющий костыль
                }
            }
        }
        

        private void AddQuadVertex(Vector2f pos, Vector2f posOnTex, int N, Chunk chunk)
        {
            chunk.vertexArray.Append(new Vertex(new Vector2f(pos.X, pos.Y), new Vector2f(posOnTex.X, posOnTex.Y)));
            chunk.vertexArray.Append(new Vertex(new Vector2f(pos.X, pos.Y + N), new Vector2f(posOnTex.X, posOnTex.Y + N)));
            chunk.vertexArray.Append(new Vertex(new Vector2f(pos.X + N, pos.Y + N), new Vector2f(posOnTex.X + N, posOnTex.Y + N)));
            chunk.vertexArray.Append(new Vertex(new Vector2f(pos.X + N, pos.Y), new Vector2f(posOnTex.X + N, posOnTex.Y)));
        }
    }
    */
}
