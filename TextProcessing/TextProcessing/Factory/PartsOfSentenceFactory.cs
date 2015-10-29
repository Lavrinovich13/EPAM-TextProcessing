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

        protected IEnumerable<string> _DelimetersCollection;

        public PartsOfSentenceFactory(IDelimetersContainer delimetersContainer)
        {
            this._DelimetersContainer = delimetersContainer;

            _SpaceDelimeter = _DelimetersContainer._SpaceDelimeter._StringValue;

            _ConnectionDelimeters = _DelimetersContainer._WordsConnectionDelimeters
                .Select(x => x._StringValue)
                .ToStringWithoutCharRepetitions();

            _UniqTextDelimeters = _DelimetersContainer._WordsDelimeters.Concat(_DelimetersContainer._SentencesDelimeters)
                .Select(x => x._StringValue)
                .ToStringWithoutCharRepetitions();

            _DelimetersCollection = _DelimetersContainer._SentencesDelimeters
                .Select(x => x._StringValue)
                .Concat(_DelimetersContainer._WordsDelimeters.Select(x => x._StringValue));

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

                if (!_DelimetersCollection.Contains(match))
                {
                    var componentPunctuation =
                    match.ToCharArray()
                    .Select(x => x.ToString()).SerialConcatBy(x => _DelimetersCollection.Contains(x));
                    foreach (var subitem in componentPunctuation)
                    {
                        partsOfSentences.Add(new Punctuation(subitem));
                    }
                }
                partsOfSentences.Add(new Punctuation(match));
            }

            return partsOfSentences.AsEnumerable();
        }
    }
}
