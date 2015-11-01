using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class PartsOfSentenceFactory : IFactory<string, IEnumerable<IPartOfSentence>>
    {
        protected IDelimetersContainer _DelimetersContainer;

        protected Regex SentencePartsRegex;
        protected Delimeter[] DelimetersInSentence;

        public PartsOfSentenceFactory(IDelimetersContainer delimetersContainer)
        {
            this._DelimetersContainer = delimetersContainer;

            var SpaceDelimeter = _DelimetersContainer.SpaceDelimeter.StringValue;

            var ConnectionDelimeters = _DelimetersContainer.WordsConnectionDelimeters
                .Select(x => x.StringValue)
                .ToRegexStringWithoutRepetitions();

            var UniqTextDelimeters = _DelimetersContainer.WordsDelimeters
                .Concat(_DelimetersContainer.SentencesDelimeters)
                .Select(x => x.StringValue)
                .ToRegexStringWithoutRepetitions();

            DelimetersInSentence = _DelimetersContainer.GetDelimetersInSentence;

            SentencePartsRegex =
               new Regex(String.Format(@"{2}*([{0}]*){2}*([\w]+[{1}]?[\w]+|[\w]+){2}*([{0}]*){2}*",
                   UniqTextDelimeters, ConnectionDelimeters, SpaceDelimeter), RegexOptions.Compiled);
        }

        public IEnumerable<IPartOfSentence> Construct(string text)
        {
            var parsedSentences = new List<ISentence>();
            var matches = SentencePartsRegex.Matches(text).GetMatchGroupsValues(3);
            var partsOfSentences = new List<IPartOfSentence>();

            foreach (var match in matches)
            {
                if (Regex.IsMatch(match, @"[\w|\d].*"))
                {
                    partsOfSentences.Add(new Word(match));
                    continue;
                }

                if (!DelimetersInSentence.Select(x => x.StringValue).Contains(match))
                {
                    var componentPunctuation =
                    match.ToCharArray()
                    .Select(x => x.ToString())
                    .SerialConcatBy(x => DelimetersInSentence.Select(y => y.StringValue).Contains(match));
                    foreach (var subitem in componentPunctuation)
                    {
                        partsOfSentences.Add(DelimetersInSentence.First(x => x.StringValue == subitem));
                    }
                    continue;
                }
                partsOfSentences.Add(DelimetersInSentence.First(x => x.StringValue == match));
            }
            return partsOfSentences.AsEnumerable();
        }
    }
}
