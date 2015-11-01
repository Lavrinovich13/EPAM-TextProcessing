using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public interface IDelimetersContainer
    {
        Delimeter SpaceDelimeter { get; }
        Delimeter[] SentencesDelimeters { get; }
        Delimeter[] WordsDelimeters { get; }
        Delimeter[] WordsConnectionDelimeters { get; }
        Delimeter[] GetDelimetersInSentence { get; }
    }
}
