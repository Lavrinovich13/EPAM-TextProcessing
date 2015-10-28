using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Delimeters : IDelimetersContainer
    {
        private string[] _SentenceDelimeters = { ".", "!", "?", "...", "?!" };
        private string[] _WordDelimeters = { ",", "(", ")", ":", ";", "'", "\"" , "-"};

        public IEnumerable<string> SentenceDelimeters { get { return _SentenceDelimeters.AsEnumerable(); } }
        public IEnumerable<string> WordDelimeters { get { return _WordDelimeters.AsEnumerable(); } }
    }
}
