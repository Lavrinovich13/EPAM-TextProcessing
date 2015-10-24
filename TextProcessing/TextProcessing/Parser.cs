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
        protected ICollection<IPartOfSentence> ParseSentence(string sentence)
        {
            return new List<IPartOfSentence>();
        }
        protected ICollection<ISentence> ParseText(string text)
        {
            string pattern = String.Format(@"([\W]*[A-Z].*?[{0}](?=\s|$))", ".!?");
            Regex regex = new Regex(pattern);
            MatchCollection sentences = regex.Matches(text);

            var parseSentences = new List<ISentence>();

            foreach(var item in sentences)
            {
                parseSentences.Add(new Sentence(ParseSentence(item.ToString())));
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
