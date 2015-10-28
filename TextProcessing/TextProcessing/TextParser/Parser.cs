using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Parser : IParser<Text, StreamReader>
    {
        protected IDelimetersContainer _DelimetersContainer;

        protected string _UniqSentencesDelimeters;
        protected string _UniqWordsDelimeters;
        protected string _UniqDelimeters;
        protected IEnumerable<string> _DelimetersCollection;

        protected Regex _SentencePartsRegex;
        protected Regex _SentenceRegex;

        public Parser(IDelimetersContainer delimetersConteiner)
        {
            this._DelimetersContainer = delimetersConteiner;

            _UniqSentencesDelimeters = _DelimetersContainer.SentenceDelimeters.ToStringWithoutRepetitions();
            _UniqWordsDelimeters = _DelimetersContainer.WordDelimeters.ToStringWithoutRepetitions();
            _UniqDelimeters = String.Concat(_UniqWordsDelimeters, _UniqSentencesDelimeters);

            _DelimetersCollection = _DelimetersContainer.SentenceDelimeters.Concat(_DelimetersContainer.WordDelimeters);

            _SentencePartsRegex = new Regex(String.Format(@"\s*([{0}]*)\s*([\w]+[\-]?[\w]+|[\w]+)\s*([{0}]*)\s*", _UniqDelimeters), RegexOptions.Compiled);
            _SentenceRegex = new Regex(String.Format(@"((.*?[{0}])\s+|\t+)(?=[\W]*[A-Z]|[А-Я])", _UniqSentencesDelimeters), RegexOptions.Compiled);
        }

        protected ICollection<IPartOfSentence> ParseSentence(string sentence)
        {
            var matches = _SentencePartsRegex.Matches(sentence).AsEnumerable(3);
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
                    match.ToCharArray().Select(x => x.ToString()).ConcatBy(x => _DelimetersCollection.Contains(x));
                    foreach (var subitem in componentPunctuation)
                    {
                        partsOfSentences.Add(new Punctuation(subitem));
                    }
                }
                partsOfSentences.Add(new Punctuation(match));
            }

            return partsOfSentences;
        }
        protected ICollection<ISentence> ParseText(string text)
        {
            var sentences = _SentenceRegex.Matches(text);
            var parseSentences = new List<ISentence>();

            var senten = new List<string>();

            foreach (Match item in sentences)
            {
                senten.Add(item.Groups[1].Value);
                parseSentences.Add(new Sentence(ParseSentence(item.Value)));
            }

            return parseSentences;
        }

        protected int NumReadLines = 4;

        public Text Parse(StreamReader stream)
        {
            int linesCounter = 0;
            string buffer = "";
            string currentLine;
            StringBuilder text = new StringBuilder();
            List<ISentence> sentences = new List<ISentence>();

            using (stream)
            {
                while ((currentLine = stream.ReadLine()) != null)
                {
                    linesCounter++;
                    if (linesCounter != NumReadLines)
                    {
                        text.Append(currentLine + " ");
                    }
                    else
                    {
                        linesCounter = 0;
                        if (!_UniqSentencesDelimeters.Contains(currentLine.Last()))
                        {
                            int lastIndex = currentLine.LastIndexOfAny(_UniqSentencesDelimeters.ToCharArray()) + 1;
                            buffer = currentLine.Substring(lastIndex);
                        }
                        text.Append(currentLine);
                        sentences.AddRange(ParseText(text.ToString()));

                        text = new StringBuilder();
                        text.Append(buffer + " ");
                    }
                }
            }

            sentences.Concat(ParseText(text.ToString()));
            return new Text(sentences);
        }
    }
}
