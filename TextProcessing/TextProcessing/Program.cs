﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            IDelimetersContainer defaultDelimeters = new DefaultDelimeters();
            Parser textParser = new Parser(defaultDelimeters);

            StreamReader reader = new StreamReader("text1.txt");
            Text text = textParser.Parse(reader);

        }
    }
}
