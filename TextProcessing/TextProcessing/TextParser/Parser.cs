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
        protected string _SpaceDelimeter;
        protected string _ConnectionDelimeters;
        protected string _UniqTextDelimeters;
        protected IEnumerable<string> _DelimetersCollection;

        protected Regex _SentencePartsRegex;
        protected Regex _SentenceRegex;

        protected int NumReadLines = 4;

        public Parser(IDelimetersContainer delimetersConteiner)
        {
            this._DelimetersContainer = delimetersConteiner;

            _SpaceDelimeter = _DelimetersContainer._SpaceDelimeter._StringValue;

            _UniqSentencesDelimeters = 
                _DelimetersContainer
                ._SentencesDelimeters
                .Select(x => x._StringValue)
                .ToStringWithoutCharRepetitions();

            _UniqWordsDelimeters =
                _DelimetersContainer
                ._WordsDelimeters
                .Select(x => x._StringValue)
                .ToStringWithoutCharRepetitions();

            _ConnectionDelimeters =
                _DelimetersContainer
                ._WordsConnectionDelimeters
                .Select(x => x._StringValue)
                .ToStringWithoutCharRepetitions();

            _UniqTextDelimeters = String.Concat(_UniqWordsDelimeters, _UniqSentencesDelimeters);

            _DelimetersCollection = 
                _DelimetersContainer
                ._SentencesDelimeters
                .Select(x => x._StringValue)
                .Concat(_DelimetersContainer._WordsDelimeters.Select(x => x._StringValue));

            _SentencePartsRegex = 
                new Regex(String.Format(@"{2}*([{0}]*){2}*([\w]+[{1}]?[\w]+|[\w]+){2}*([{0}]*){2}*",
                    _UniqTextDelimeters, _ConnectionDelimeters, _SpaceDelimeter), RegexOptions.Compiled);

            _SentenceRegex = 
                new Regex(String.Format(@"((?<=.*?[{0}]){1}+|\t+)(?=[\W]*[A-Z]|[А-Я])",
                    _UniqSentencesDelimeters, _SpaceDelimeter), RegexOptions.Compiled);
        }

        protected IEnumerable<ISentence> ParseSentences(string[] sentences)
        {
            var parsedSentences = new List<ISentence>();
            foreach (var sentence in sentences.Where(x => !String.IsNullOrWhiteSpace(x)))
            {
                var matches = _SentencePartsRegex.Matches(sentence).GetMatchGroupsValues(3);
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

                parsedSentences.Add(new Sentence(partsOfSentences));
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
                       foreach(string s in splitSentences)
                       {
                           Console.WriteLine(s);
                       }
                        text.Clear();
                        sentences.AddRange(ParseSentences(splitSentences));
                        Console.WriteLine(sentences.Count());
                    }
                }
            }
            var splitSentencess = _SentenceRegex.Split(text.ToString());
            splitSentencess[0].Insert(0, buffer + _SpaceDelimeter);
            sentences.AddRange(ParseSentences(splitSentencess));
            foreach (string s in splitSentencess)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(sentences.Count());
            return new Text(sentences);
        }
    }
}
