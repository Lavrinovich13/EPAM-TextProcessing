using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class Word : IWord
    {
        public Symbol[] CharValue { get; private set; }
        public string StringValue 
        {
            get
            {
                return String.Join(String.Empty, CharValue.Select(x => x.GetSymbol).ToArray());
            }
        }
        public Word(string word)
        {
            CharValue = word.Select(x => new Symbol(x)).ToArray();
        }

        public int Length
        {
            get
            {
                return CharValue == null ? 0 : CharValue.Length;
;
            }
        }

        public bool IsStartsWithVowel()
        {
            char[] VowelsArray = new char[] { 'a', 'o', 'e', 'y', 'u', 'i' };
            return VowelsArray.Contains(StringValue[0]) ? true : false;
        }
    }
}
