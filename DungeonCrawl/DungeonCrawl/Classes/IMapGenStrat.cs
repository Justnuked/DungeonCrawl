using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    interface IMapGenStrat<T> where T : IMap
    {
        T CreateMap();
    }
}
