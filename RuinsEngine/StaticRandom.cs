using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsEngine
{
    public class staticRandom
    {
        public static Random random = new Random();

        public static bool Rand100(int percent)// если число от 0 до 100 больше процента то false
        {
            if (random.Next(0, 100) > percent) return false;
            return true;
        }
    }
}
