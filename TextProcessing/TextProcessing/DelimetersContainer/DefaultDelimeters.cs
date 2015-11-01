using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class DefaultDelimeters : IDelimetersContainer
    {
        public Delimeter SpaceDelimeter { get; private set; }
        public Delimeter[] SentencesDelimeters { get; private set; }
        public Delimeter[] WordsDelimeters { get; private set; }
        public Delimeter[] WordsConnectionDelimeters { get; private set; }
        

        public DefaultDelimeters()
        {
            SpaceDelimeter = new Delimeter(" ", false, DelimeterTypes.Single);

            SentencesDelimeters = new Delimeter[]
            {
                new Delimeter(".", false, DelimeterTypes.Single, SentenceTypes.Declarative),
                new Delimeter("!", false, DelimeterTypes.Single, SentenceTypes.Interrogative),
                new Delimeter("?", false, DelimeterTypes.Single, SentenceTypes.Exclamatory),
                new Delimeter("...", false, DelimeterTypes.Single, SentenceTypes.Declarative),
                new Delimeter("?!", false, DelimeterTypes.Single, SentenceTypes.Exclamatory),
                new Delimeter("!?", false, DelimeterTypes.Single, SentenceTypes.Exclamatory)
            };

            WordsDelimeters = new Delimeter[] 
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

            WordsConnectionDelimeters = new Delimeter[] 
            {
                new Delimeter("-", false, DelimeterTypes.Single),
                new Delimeter("'", false, DelimeterTypes.Single)
            };
        }

        public Delimeter[] GetDelimetersInSentence
        {
           get
            {
                return SentencesDelimeters.Concat(WordsDelimeters).ToArray();
            }
        }
    }
}
