using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    class LineReader : ILineReader
    {
        public IEnumerable<string> Read(string file)
        {
            return System.IO.File.ReadAllLines(file);
        }
    }
}
