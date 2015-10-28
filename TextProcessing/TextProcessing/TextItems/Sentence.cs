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

        public SentenceTypes _SentenceType
        {
            get { return SentenceTypes.Declarative; }
        }

        public Sentence()
        {
            _Items = new List<IPartOfSentence>();
        }

        public Sentence(ICollection<IPartOfSentence> items)
        {
            _Items = items;
        }

        public int GetNumberOf(Type type)
        {
            return _Items.Where(x => x.GetType() == type).Count();
        }

        public IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate)
        {
            return _Items.Where(x => x is IWord)
                .Cast<IWord>()
                .Where(x => predicate(x))
                .Distinct()
                .AsEnumerable();
        }

        public IEnumerable<IWord> RemoveWordsBy(Func<IWord, bool> predicate)
        {
            return _Items.Where(x => x is IWord).Cast<IWord>().Where(x => !predicate(x)).AsEnumerable();
        }

    }
}
