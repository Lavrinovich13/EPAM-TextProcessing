using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    interface IDelimetersContainer
    {
        Delimeter _SpaceDelimeter { get; }
        Delimeter[] _SentencesDelimeters { get; }
        Delimeter[] _WordsDelimeters { get; }
        Delimeter[] _WordsConnectionDelimeters { get; }
        Delimeter[] GetDelimetersInSentence();
    }
}
