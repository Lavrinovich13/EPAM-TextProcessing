using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Sentence : ISentence
    {
        public IEnumerable<IPartOfSentence> _Items { get; private set; }

        public SentenceTypes _SentenceType
        {
            get { return SentenceTypes.Declarative; }
        }

        public Sentence()
        {
            _Items = new List<IPartOfSentence>();
        }

        public Sentence(IEnumerable<IPartOfSentence> items)
        {
            _Items = items;
        }

        public int GetNumberOfWords()
        {
            return _Items.Where(x => x is IWord).Count();
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

        public IEnumerable<IWord> ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IPartOfSentence> parser)
        {
            return new List<IWord>();
        }
    }
}
