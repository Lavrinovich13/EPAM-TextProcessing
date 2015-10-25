﻿using System;
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
        public Word(string word)
        {
            _Value = word.ToCharArray().Select(x => new Symbol(x)).ToArray();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
