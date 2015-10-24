using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    class Symbol
    {
        public string _Value { get; private set; }

        public Symbol(char symbol)
        {
            _Value = symbol.ToString();
        }

        public Symbol(string symbol)
        {
            _Value = symbol;
        }
    }
}
