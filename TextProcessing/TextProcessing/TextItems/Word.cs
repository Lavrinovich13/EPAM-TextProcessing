using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Word : IWord
    {
        public string _StringValue { get; private set; }
        public Word(string word)
        {
            _StringValue = word;
        }

        public int Length
        {
            get
            {
                return _StringValue.Length;
            }
        }

        public bool IsStartsWithVowel()
        {
            char[] VowelsArray = new char[] { 'a', 'o', 'e', 'y', 'u', 'i' };
            return VowelsArray.Contains(_StringValue[0]) ? true : false;
        }
    }
}
