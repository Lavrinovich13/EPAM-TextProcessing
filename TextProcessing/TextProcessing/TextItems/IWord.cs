using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    interface IWord : IPartOfSentence, IEquatable<IWord>
    {
        Symbol[] _Value { get; }
        int Length { get; }
        bool IsStartsWith(Func<Symbol, bool> predicate);
    }
}
