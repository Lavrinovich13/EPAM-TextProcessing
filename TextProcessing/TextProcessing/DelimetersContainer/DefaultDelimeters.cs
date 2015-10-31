using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class DefaultDelimeters : IDelimetersContainer
    {
        public Delimeter _SpaceDelimeter { get; private set; }
        public Delimeter[] _SentencesDelimeters { get; private set; }
        public Delimeter[] _WordsDelimeters { get; private set; }
        public Delimeter[] _WordsConnectionDelimeters { get; private set; }
        

        public DefaultDelimeters()
        {
            _SpaceDelimeter = new Delimeter(" ", false, DelimeterTypes.Single);

            _SentencesDelimeters = new Delimeter[]
            {
                new Delimeter(".", false, DelimeterTypes.Single, SentenceTypes.Declarative),
                new Delimeter("!", false, DelimeterTypes.Single, SentenceTypes.Interrogative),
                new Delimeter("?", false, DelimeterTypes.Single, SentenceTypes.Exclamatory),
                new Delimeter("...", false, DelimeterTypes.Single, SentenceTypes.Declarative),
                new Delimeter("?!", false, DelimeterTypes.Single, SentenceTypes.Exclamatory),
                new Delimeter("!?", false, DelimeterTypes.Single, SentenceTypes.Exclamatory)
            };

            _WordsDelimeters = new Delimeter[] 
            {
                new Delimeter(",", false, DelimeterTypes.Single),
                new Delimeter("(", false, DelimeterTypes.FirstInPair),
                new Delimeter(")", false, DelimeterTypes.LastInPair),
                new Delimeter("{", false, DelimeterTypes.FirstInPair),
                new Delimeter("}", false, DelimeterTypes.LastInPair),
                new Delimeter("[", false, DelimeterTypes.FirstInPair),
                new Delimeter("]", false, DelimeterTypes.LastInPair),
                new Delimeter(":", false, DelimeterTypes.Single),
                new Delimeter(";", false, DelimeterTypes.Single),
                new Delimeter("'", false, DelimeterTypes.Double),
                new Delimeter("\"", false, DelimeterTypes.Double),
                new Delimeter("-", true, DelimeterTypes.Single)
            };

            _WordsConnectionDelimeters = new Delimeter[] 
            {
                new Delimeter("-", false, DelimeterTypes.Single),
                new Delimeter("'", false, DelimeterTypes.Single)
            };
        }

        public Delimeter[] GetDelimetersInSentence()
        {
            return _SentencesDelimeters.Concat(_WordsDelimeters).ToArray();
        }
    }
}
