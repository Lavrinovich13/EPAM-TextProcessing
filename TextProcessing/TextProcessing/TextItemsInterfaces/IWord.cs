using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public interface IWord : IPartOfSentence
    {
        Symbol[] CharValue { get; }
        int Length { get; }
        bool IsStartsWithVowel();
    }
}
