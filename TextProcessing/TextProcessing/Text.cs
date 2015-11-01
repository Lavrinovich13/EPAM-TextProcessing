using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class Text
    {
        private ICollection<ISentence> Sentences;

        public IEnumerable<ISentence> GetSentences
        {
            get
            {
                return Sentences.AsEnumerable();
            }
        }

        public Text(ICollection<ISentence> sentences)
        {
            Sentences = sentences;
        }

        public IEnumerable<IWord> GetDistinctWordsBy
            (Func<ISentence, bool> sentencePredicate, Func<IWord, bool> wordPredicate)
        {
            var words = new List<IWord>();

            foreach (var sentence in Sentences.Where(x => x.Type == SentenceTypes.Exclamatory))
            {
                words.AddRange(sentence.GetWordsBy(x => x.Length == 3));
            }

            return words.GroupBy(x => x.StringValue.ToLowerInvariant()).Select(x => x.First());
        }

        public void RemoveWordsBy(Func<IWord, bool> predicate)
        {
            foreach (var sentence in Sentences)
            {
                sentence.RemoveWordsBy(predicate);
            }
        }

        public void ReplaceWordsBy
            (int sentenceIndex, string newString, Func<IWord, bool> predicate, IFactory<string, IEnumerable<IPartOfSentence>> partsOfSentenceFactory)
        {
            Sentences.ElementAt(sentenceIndex).ReplaceWordsBy(predicate, newString, partsOfSentenceFactory);
        }
    }
}
