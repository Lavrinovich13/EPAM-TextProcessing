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
        public Word(string word)
        {
            _Value = word.ToCharArray().Select(x => new Symbol(x)).ToArray();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }

        public int Length
        {
            get
            {
                return _Value.Length;
            }
        }

        public bool IsStartsWith(Func<Symbol,bool> predicate)
        {
            return _Value.Length == 1 ? predicate(_Value[0]) : false;
        }

        public bool Equals(IWord other)
        {
            return this._Value.SequenceEqual(this._Value);
        }
    }
}
