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

        public SentenceTypes _Type
        {
            get { return (_Items.Last() as IDelimeter)._SentenceType; }
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
            return _Items
                .Where(x => x is IWord)
                .Count();
        }

        public IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate)
        {
            return _Items.Where(x => x is IWord)
                .Cast<IWord>()
                .Where(x => predicate(x))
                .AsEnumerable();
        }

        public IEnumerable<IPartOfSentence> RemoveWordsBy(Func<IWord, bool> predicate)
        {
            return _Items
                .Where(x => !(x is IWord && predicate(x as IWord)))
                .AsEnumerable();
        }

        public IEnumerable<IPartOfSentence> ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IPartOfSentence> parser)
        {
            var newParts = parser.Build(replaceString);

            var newSentence = new List<IPartOfSentence>();

            foreach(var part in _Items)
            {
                if (part is IWord)
                {
                    if (predicate(part as IWord))
                    {
                        newSentence.AddRange(newParts);
                        continue;
                    }
                }
               newSentence.Add(part);
            }

            return newSentence.AsEnumerable();
        }
    }
}
