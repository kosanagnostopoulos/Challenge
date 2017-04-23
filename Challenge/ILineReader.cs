using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    internal interface ILineReader
    {
        IEnumerable<string> Read(string file);
    }
}
