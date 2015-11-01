using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class Sentence : ISentence
    {
        public IEnumerable<IPartOfSentence> Items { get; private set; }

        public SentenceTypes Type
        {
            get { return (Items.Last() as IDelimeter).SentenceType; }
        }

        public Sentence()
        {
            Items = new List<IPartOfSentence>();
        }

        public Sentence(IEnumerable<IPartOfSentence> items)
        {
            this.Items = items;
        }

        public int GetNumberOfWords()
        {
            return Items
                .Where(x => x is IWord)
                .Count();
        }

        public IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate)
        {
            return Items.Where(x => x is IWord)
                .Cast<IWord>()
                .Where(x => predicate(x))
                .AsEnumerable();
        }

        public void RemoveWordsBy(Func<IWord, bool> predicate)
        {
            Items = Items
               .Where(x => !(x is IWord && predicate(x as IWord)))
               .AsEnumerable();
        }

        public void ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IEnumerable<IPartOfSentence>> parser)
        {
            var newParts = parser.Construct(replaceString);

            var newSentence = new List<IPartOfSentence>();

            foreach (var part in Items)
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

            Items = newSentence.AsEnumerable();
        }
    }
}
