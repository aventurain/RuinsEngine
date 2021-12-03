using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace RuinsEngine
{
    public class FontsWorker
    {
        Dictionary<string,Font> fonts = new Dictionary<string,Font>();

        public void AddFont(string path)
        {
            Font font = new Font(path);
            if(font != null)
            {
                string name = path.Split('/').Last() ;
                name = name.Split('.')[0];
                fonts.Add(name, font);
            }
        }

        public Font GetFont(string name)// Like GetFont(Arial)
        {
            return fonts[name];
        }
    }
}
