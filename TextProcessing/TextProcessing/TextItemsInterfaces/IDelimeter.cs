using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    interface IDelimeter : IPartOfSentence
    {
        bool _IsSeparatedBySpace { get; }
        DelimeterTypes _DelimeterType { get; }
        SentenceTypes _SentenceType { get; }
    }
}
