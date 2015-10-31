using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    
    class Delimeter : IDelimeter
    {
        public string _StringValue { get; private set; }
        public bool _IsSeparatedBySpace { get; private set; }
        public DelimeterTypes _DelimeterType { get; private set; }
        public SentenceTypes _SentenceType { get; private set; }

        public Delimeter
            (string value, bool isSeparatedBySpace,
              DelimeterTypes delimeterType, SentenceTypes sentenceType = SentenceTypes.Indefinite)
        {
            this._StringValue = value;
            this._IsSeparatedBySpace = isSeparatedBySpace;
            this._DelimeterType = delimeterType;
            this._SentenceType = sentenceType;
        }
    }
}
