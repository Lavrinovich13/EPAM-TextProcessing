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
        protected IFactory<string, IPartOfSentence> _PartsOfSentenceFactory;

        protected string _UniqSentencesDelimeters;
        protected string _SpaceDelimeter;

        protected Regex _SentenceRegex;

        protected int NumReadLines = 4;

        public Parser(IDelimetersContainer delimetersConteiner, IFactory<string, IPartOfSentence> factory)
        {
            this._DelimetersContainer = delimetersConteiner;
            this._PartsOfSentenceFactory = factory;

            _SpaceDelimeter = _DelimetersContainer._SpaceDelimeter._StringValue;

            _UniqSentencesDelimeters = _DelimetersContainer._SentencesDelimeters
                .Select(x => x._StringValue)
                .ToRegexStringWithoutRepetitions();

            _SentenceRegex = 
                new Regex(String.Format(@"((?<=.*?[{0}]){1}+|\t+)(?=[\W]*[A-Z]|[А-Я])",
                    _UniqSentencesDelimeters, _SpaceDelimeter), RegexOptions.Compiled);
        }

        protected IEnumerable<ISentence> ParseSentences(string[] sentences)
        {
            var parsedSentences = new List<ISentence>();
            foreach (var sentence in sentences.Where(x => !String.IsNullOrWhiteSpace(x)))
            {
                parsedSentences.Add(new Sentence(_PartsOfSentenceFactory.Build(sentence)));
            }
            return parsedSentences;
        }

        public Text Parse(StreamReader stream)
        {
            int linesCounter = 0;
            string buffer = String.Empty;
            string currentLine;
            StringBuilder text = new StringBuilder();
            List<ISentence> sentences = new List<ISentence>();

            using (stream)
            {
                while ((currentLine = stream.ReadLine()) != null)
                {
                    text.Append(currentLine + _SpaceDelimeter);
                    linesCounter++;

                    if (linesCounter == NumReadLines)
                    {
                        linesCounter = 0;
                        var splitSentences = _SentenceRegex.Split(text.ToString());
                        
                        splitSentences[0].Insert(0, buffer + _SpaceDelimeter);

                        if (!_UniqSentencesDelimeters.Contains(splitSentences.Last().Trim().Last()))
                        {
                            buffer = splitSentences.Last();
                            splitSentences[splitSentences.Length - 1] = String.Empty;
                        }
                        text.Clear();
                        sentences.AddRange(ParseSentences(splitSentences));
                    }
                }
            }
            var splitSentencess = _SentenceRegex.Split(text.ToString());
            splitSentencess[0].Insert(0, buffer + _SpaceDelimeter);
            sentences.AddRange(ParseSentences(splitSentencess));
            return new Text(sentences);
        }
    }
}
