using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hexagonal
{
    public class MapData
    {
        public class Map
        {
            public string name;
            public int width;
            public int height;
            public List<Grid> grids;
        }
        public class Grid
        {
            public int id;
            public int state;
        }

        public string name;
        public int width;
        public int height;
        public Grid[,] grids;

        public MapData(string name)
        {
            LoadMapData(name);
        }
        public MapData(string name,int width,int height)
        {
            Initialize(name,width,height);
        }
        private void Initialize(string name, int width, int height)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.grids = new Grid[width,height];
            for (int w = 0; w < width;w++)
            {
                for(int h = 0; h < height; h++)
                {
                    grids[w, h] = new Grid();
                    grids[w, h].id = ((1000000 + (w * 1000)) + h);
                    grids[w, h].state = 0;
                }
            }
        }
        private void LoadMapData(string filename)
        {
            string text = System.IO.File.ReadAllText(filename);

            JObject jo = JObject.Parse(text);

            this.name = jo["name"].ToString();
            this.width = int.Parse(jo["width"].ToString());
            this.height = int.Parse(jo["height"].ToString());
            this.grids = new Grid[width, height];
            string grids = jo["grids"].ToString();
            JArray ja = (JArray)JsonConvert.DeserializeObject(grids);

            foreach (JObject item in ja)
            {
                int id = int.Parse(item["id"].ToString());
                int state = int.Parse(item["state"].ToString());
                int h = id % 1000;
                int w = ((id - h) / 1000)% 1000;
                this.grids[w, h] = new Grid();
                this.grids[w, h].id = id;
                this.grids[w, h].state = state;
            }
        }

        public void setGridStae(int row,int col,int state)
        {
            grids[row, col].state = state;
        }
        public int getGridStae(int row,int col)
        {
            return grids[row, col].state;
        }

        public void writeDataToFile(StreamWriter w)
        {
            Map map = new Map();
            map.name = this.name;
            map.width = this.width;
            map.height = this.height;
            map.grids = new List<Grid>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Grid target = grids[i,j];
                    map.grids.Insert(0, target);
                }
            }

            string json1 = JsonConvert.SerializeObject(map);
            w.Write(json1);
            //w.Write("\n}");
        }

    }
}
