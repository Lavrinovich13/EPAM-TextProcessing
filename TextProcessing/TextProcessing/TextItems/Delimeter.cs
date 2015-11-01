using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    public class Delimeter : IDelimeter
    {
        public Symbol CharValue { get; private set; }
        public bool IsSeparatedBySpace { get; private set; }
        public DelimeterTypes DelimeterType { get; private set; }
        public SentenceTypes SentenceType { get; private set; }

        public Delimeter
            (string value, bool isSeparatedBySpace,
              DelimeterTypes delimeterType, SentenceTypes sentenceType = SentenceTypes.Indefinite)
        {

            this.CharValue = new Symbol(value);
            this.IsSeparatedBySpace = isSeparatedBySpace;
            this.DelimeterType = delimeterType;
            this.SentenceType = sentenceType;
        }

        public string StringValue
        {
            get
            {
                return CharValue.GetSymbol;
            }
        }
    }
}
