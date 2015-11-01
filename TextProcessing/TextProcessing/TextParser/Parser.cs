using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class Parser : IParser<StreamReader, Text>
    {
        protected IDelimetersContainer DelimetersContainer;
        protected IFactory<string, IEnumerable<IPartOfSentence>> PartsOfSentenceFactory;

        protected string UniqSentencesDelimeters;
        protected string SpaceDelimeter;

        protected Regex SentenceRegex;

        public Parser(IDelimetersContainer delimetersConteiner, IFactory<string, IEnumerable<IPartOfSentence>> factory)
        {
            this.DelimetersContainer = delimetersConteiner;
            this.PartsOfSentenceFactory = factory;

            SpaceDelimeter = DelimetersContainer.SpaceDelimeter.StringValue;

            UniqSentencesDelimeters = DelimetersContainer.SentencesDelimeters
                .Select(x => x.StringValue)
                .ToRegexStringWithoutRepetitions();

            SentenceRegex =
                new Regex(String.Format(@"((?<=.*?[{0}]){1}+|\t+)(?=[\W]*[A-Z]|[А-Я])",
                    UniqSentencesDelimeters, SpaceDelimeter), RegexOptions.Compiled);
        }

        protected IEnumerable<ISentence> ParseSentences(string[] sentences)
        {
            var parsedSentences = new List<ISentence>();
            foreach (var sentence in sentences.Where(x => !String.IsNullOrWhiteSpace(x)))
            {
                parsedSentences.Add(new Sentence(PartsOfSentenceFactory.Construct(sentence)));
            }
            return parsedSentences;
        }

        public Text Parse(StreamReader stream)
        {
            string buffer = String.Empty;
            string currentLine;
            StringBuilder text = new StringBuilder();
            List<ISentence> sentences = new List<ISentence>();

            using (stream)
            {
                while ((currentLine = stream.ReadLine()) != null)
                {
                    text.Append(buffer + SpaceDelimeter + currentLine);

                    var splitSentences = SentenceRegex
                        .Split(text.ToString())
                        .Where(x => !String.IsNullOrWhiteSpace(x))
                        .ToArray();

                    if (splitSentences.Count() != 0 && !UniqSentencesDelimeters.Contains(splitSentences.Last().Trim().Last()))
                    {
                        buffer = splitSentences.Last();
                        splitSentences[splitSentences.Length - 1] = String.Empty;
                    }
                    text.Clear();
                    sentences.AddRange(ParseSentences(splitSentences));
                }
            }
            var splitSentencess = SentenceRegex.Split(text.ToString());
            splitSentencess[0].Insert(0, buffer + SpaceDelimeter);
            sentences.AddRange(ParseSentences(splitSentencess));
            return new Text(sentences);
        }
    }
}
