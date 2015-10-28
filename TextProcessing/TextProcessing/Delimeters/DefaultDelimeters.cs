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
            _SpaceDelimeter = new Delimeter(" ", false, false);

            _SentencesDelimeters = new Delimeter[]
            {
                new Delimeter(".", false, false),
                new Delimeter("!", false, false),
                new Delimeter("?", false, false),
                new Delimeter("...", false, false),
                new Delimeter("?!", false, false),
                new Delimeter("!?", false, false)
            };

            _WordsDelimeters = new Delimeter[] 
            {
                new Delimeter(",", false, false),
                new Delimeter("(", false, true),
                new Delimeter(")", false, true),
                new Delimeter(":", false, false),
                new Delimeter(";", false, false),
                new Delimeter("'", false, true),
                new Delimeter("\"", false, true),
                new Delimeter("-", true, false)
            };

            _WordsConnectionDelimeters = new Delimeter[] 
            {
                new Delimeter("-", false, false),
                new Delimeter("'", false, false)
            };

        }
    }
}
