using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Parser : IParser<Text, string>
    {
        protected IDelimetersContainer _DelimetersContainer;

        protected string _UniqSentencesDelimeters;
        protected string _UniqWordsDelimeters;
        protected string _UniqDelimeters;
        protected IEnumerable<string> _DelimetersCollection;

        protected Regex _WordRegex;
        protected Regex _SentenceRegex;

        public Parser(IDelimetersContainer delimetersConteiner)
        {
            this._DelimetersContainer = delimetersConteiner;

            _UniqSentencesDelimeters = _DelimetersContainer.SentenceDelimeters.ToStringWithoutRepetitions();
            _UniqWordsDelimeters = _DelimetersContainer.WordDelimeters.ToStringWithoutRepetitions();
            _UniqDelimeters = String.Concat(_UniqWordsDelimeters, _UniqSentencesDelimeters);

            _DelimetersCollection = _DelimetersContainer.SentenceDelimeters.Concat(_DelimetersContainer.WordDelimeters);

            _WordRegex = new Regex(String.Format(@"\s*([{0}]*)([\w]+)([{0}]*)\s*", _UniqDelimeters), RegexOptions.Compiled);
            _SentenceRegex = new Regex(String.Format(@"([\W]*.*?[{0}])\s+|$", _UniqSentencesDelimeters), RegexOptions.Compiled);
        }

        protected ICollection<IPartOfSentence> ParseSentence(string sentence)
        {
            var matches = _WordRegex.Matches(sentence).AsEnumerable(3);
            var partsOfSentences = new List<IPartOfSentence>();

            foreach (var item in matches)
            {
                if(Regex.IsMatch(item, @"[\w]+"))
                {
                    partsOfSentences.Add(new Word(item));
                    continue;
                }

                if (!_DelimetersCollection.Contains(item))
                {
                    var componentPunctuation =
                    item.ToCharArray().Select(x => x.ToString()).ConcatBy(x => _DelimetersCollection.Contains(x));
                    foreach (var subitem in componentPunctuation)
                    {
                        partsOfSentences.Add(new Punctuation(subitem));
                    }
                }
                partsOfSentences.Add(new Punctuation(item));
            }

            return partsOfSentences;
        }
        protected ICollection<ISentence> ParseText(string text)
        {
            Func<string, bool> concatCondition = x => Char.IsLower(x.FirstOrDefault(y => Char.IsLetter(y)));

            var sentences = _SentenceRegex.Matches(text).AsEnumerable(1).ConcatBy(concatCondition);
            var parseSentences = new List<ISentence>();

            foreach(var item in sentences)
            {
                parseSentences.Add(new Sentence(ParseSentence(item)));
            }

            return parseSentences;
        }
        public Text Parse(string text)
        {
            var sentences = ParseText(text);
            return new Text(sentences);
        }
    }
}
