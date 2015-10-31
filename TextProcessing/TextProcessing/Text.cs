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

        public IEnumerable<ISentence> GetSentences()
        {
            return _Sentences.AsEnumerable();
        }

        public Text()
        {
            _Sentences = new List<ISentence>();
        }

        public Text(ICollection<ISentence> sentences)
        {
            _Sentences = sentences;
        }

        public IEnumerable<IWord> GetDistinctWordsBy
            (Func<ISentence, bool> sentencePredicate, Func<IWord, bool> wordPredicate)
        {
            var words = new List<IWord>();

            foreach (var sentence in _Sentences.Where(x => x._Type == SentenceTypes.Exclamatory))
            {
                words.AddRange(sentence.GetWordsBy(x => x.Length == 3));
            }

            return words.GroupBy(x => x._StringValue.ToLowerInvariant()).Select(x => x.First());
        }

        public void RemoveWordsBy(Func<IWord, bool> predicate)
        {
            _Sentences = _Sentences.Select(x => new Sentence(x.RemoveWordsBy(predicate))).ToList<ISentence>();
        }

        public void ReplaceWordsBy
            (Func<IWord, bool> predicate, int sentenceIndex, string newString, IFactory<string, IPartOfSentence> partsOfSentenceFactory)
        {
            _Sentences.ToList()[sentenceIndex] = 
                new Sentence(_Sentences
                    .ElementAt(sentenceIndex)
                    .ReplaceWordsBy(x => x.Length == 3, newString, partsOfSentenceFactory));
        }
    }
}
