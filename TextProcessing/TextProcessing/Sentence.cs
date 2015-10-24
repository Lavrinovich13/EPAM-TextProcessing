using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Sentence : ISentence
    {
        private ICollection<IPartOfSentence> _Items;

        public Sentence()
        {
            _Items = new List<IPartOfSentence>();
        }

        public Sentence(ICollection<IPartOfSentence> items)
        {
            _Items = items;
        }

    }
}
