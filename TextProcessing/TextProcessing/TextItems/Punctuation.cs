using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Punctuation : IPunctuation
    {
        public Symbol _Value { get; private set; }

        public Punctuation(Symbol punctuation)
        {
            _Value = punctuation;
        }

        public Punctuation(string punctuation)
        {
            _Value = new Symbol(punctuation);
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
