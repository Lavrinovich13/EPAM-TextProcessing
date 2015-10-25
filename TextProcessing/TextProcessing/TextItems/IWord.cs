using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    interface IWord : IPartOfSentence
    {
        Symbol[] _Value { get; }
    }
}
