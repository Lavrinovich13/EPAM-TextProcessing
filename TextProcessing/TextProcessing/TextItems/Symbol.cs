using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public struct Symbol
    {
        private string _Value;

        public string GetSymbol 
        {
            get
            {
                return _Value;
            }
        }

        public Symbol(string symbol)
        {
            _Value = symbol;
        }

        public Symbol(char symbol)
        {
            _Value = symbol.ToString();
        }
    }
}
