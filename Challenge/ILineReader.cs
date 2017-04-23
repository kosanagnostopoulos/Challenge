﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public interface ILineReader
    {
        IEnumerable<string> Read(string file);
    }
}
