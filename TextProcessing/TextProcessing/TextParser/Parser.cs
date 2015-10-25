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

        public Parser(IDelimetersContainer delimetersConteiner)
        {
            this._DelimetersContainer = delimetersConteiner;
        }

        protected ICollection<IPartOfSentence> ParseSentence(string sentence)
        {
            string delimeters = String.Concat(_DelimetersContainer.WordDelimeters.ToStringWithoutRepetitions(),
                                _DelimetersContainer.SentenceDelimeters.ToStringWithoutRepetitions());

            var delimetersArray = _DelimetersContainer.SentenceDelimeters.Concat(_DelimetersContainer.WordDelimeters);

            string pattern = String.Format(@"\s*([{0}]*)([\w]+)([{0}]*)\s*", delimeters);

            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            var matches = regex.Matches(sentence).AsEnumerable(3);
            
            List<IPartOfSentence> partsOfSentences = new List<IPartOfSentence>();

            foreach (var item in matches)
            {
                if(Regex.IsMatch(item, @"[\w]+"))
                {
                    partsOfSentences.Add(new Word(item));
                    continue;
                }

                if (!delimetersArray.Contains(item))
                {
                    var componentPunctuation = 
                        item.ToCharArray().Select(x => x.ToString()).ConcatBy(x => delimetersArray.Contains(x));
                    foreach(var subitem in componentPunctuation)
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
            //TODO catch exceptions
            string delimeters = _DelimetersContainer.SentenceDelimeters.ToStringWithoutRepetitions();
            string pattern = String.Format(@"([\W]*.*?[{0}])\s+|$", delimeters);

            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            //TODO change condition
            Func<string, bool> concatCondition = x => Char.IsLower(x.FirstOrDefault(y => Char.IsLetter(y)));

            var sentences = regex.Matches(text).AsEnumerable(1).ConcatBy(concatCondition);
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
