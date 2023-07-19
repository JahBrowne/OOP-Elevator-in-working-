using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorWork
{
    class Level
    {
        int Floor;
        int LevelstartlocationY;

        public Level(int Num, int location)
        {
            Floor = Num;
            LevelstartlocationY = location;
        }

        internal int GetLevelYLocation()
        {
            return LevelstartlocationY;
        }

        internal int GetFloor()
        {
            return Floor;
        }

        internal void Open_()
        {

        }

    }
}
