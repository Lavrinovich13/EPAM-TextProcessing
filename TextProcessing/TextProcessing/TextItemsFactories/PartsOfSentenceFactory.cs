using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    class PartsOfSentenceFactory : IFactory<string, IPartOfSentence>
    {
        protected IDelimetersContainer _DelimetersContainer;

        protected Regex _SentencePartsRegex;
        protected string _SpaceDelimeter;
        protected string _ConnectionDelimeters;
        protected string _UniqTextDelimeters;

        protected Delimeter[] _DelimetersInSentence;

        public PartsOfSentenceFactory(IDelimetersContainer delimetersContainer)
        {
            this._DelimetersContainer = delimetersContainer;

            _SpaceDelimeter = _DelimetersContainer._SpaceDelimeter._StringValue;

            _ConnectionDelimeters = _DelimetersContainer._WordsConnectionDelimeters
                .Select(x => x._StringValue)
                .ToRegexStringWithoutRepetitions();

            _UniqTextDelimeters = _DelimetersContainer._WordsDelimeters.Concat(_DelimetersContainer._SentencesDelimeters)
                .Select(x => x._StringValue)
                .ToRegexStringWithoutRepetitions();

            _DelimetersInSentence = _DelimetersContainer.GetDelimetersInSentence();

            _SentencePartsRegex =
               new Regex(String.Format(@"{2}*([{0}]*){2}*([\w]+[{1}]?[\w]+|[\w]+){2}*([{0}]*){2}*",
                   _UniqTextDelimeters, _ConnectionDelimeters, _SpaceDelimeter), RegexOptions.Compiled);
        }

        public IEnumerable<IPartOfSentence> Build(string text)
        {
            var parsedSentences = new List<ISentence>();
            var matches = _SentencePartsRegex.Matches(text).GetMatchGroupsValues(3);
            var partsOfSentences = new List<IPartOfSentence>();

            foreach (var match in matches)
            {
                if (Regex.IsMatch(match, @"[\w|\d].*"))
                {
                    partsOfSentences.Add(new Word(match));
                    continue;
                }

                if (!_DelimetersInSentence.Select(x => x._StringValue).Contains(match))
                {
                    var componentPunctuation =
                    match.ToCharArray()
                    .Select(x => x.ToString()).SerialConcatBy(x => _DelimetersInSentence.Select(y => y._StringValue).Contains(match));
                    foreach (var subitem in componentPunctuation)
                    {
                        partsOfSentences.Add(_DelimetersInSentence.First(x => x._StringValue == subitem));
                    }
                    continue;
                }
                partsOfSentences.Add(_DelimetersInSentence.First(x => x._StringValue == match));
            }

            return partsOfSentences.AsEnumerable();
        }
    }
}
