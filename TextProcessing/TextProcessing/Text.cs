using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Text
    {
        private ICollection<ISentence> _Sentences;

        public Text()
        {
            _Sentences = new List<ISentence>();
        }

        public Text(ICollection<ISentence> sentences)
        {
            _Sentences = sentences;
        }
    }
}
