using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Word : IWord
    {
        public Symbol[] _Value { get; private set; }

        public Word(Symbol[] word)
        {
            _Value = word;
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
