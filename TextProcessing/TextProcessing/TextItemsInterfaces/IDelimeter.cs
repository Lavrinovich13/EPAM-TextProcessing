using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public interface IDelimeter : IPartOfSentence
    {
        Symbol CharValue { get; }
        bool IsSeparatedBySpace { get; }
        DelimeterTypes DelimeterType { get; }
        SentenceTypes SentenceType { get; }
    }
}
