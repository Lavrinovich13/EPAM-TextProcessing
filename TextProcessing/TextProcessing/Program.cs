using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            IDelimetersContainer defaultDelimeters = new DefaultDelimeters();

            IFactory<string, IEnumerable<IPartOfSentence>> partsOfSentenceFactory
                = new PartsOfSentenceFactory(defaultDelimeters);

            Parser textParser = new Parser(defaultDelimeters, partsOfSentenceFactory);

            try
            {
                Text text = null;
                using (StreamReader reader = new StreamReader("text.txt"))
                {
                    text = textParser.Parse(reader);
                }

                TextToStreamWriter textWriter = new TextToStreamWriter();
                textWriter.SpaceDelimeter = defaultDelimeters.SpaceDelimeter;
                using (StreamWriter writer = new StreamWriter("result.txt"))
                {
                    writer.WriteLine("-> Sorted sentences");
                    var sortedSentences = text.GetSentences.OrderBy(x => x.GetNumberOfWords()).AsEnumerable();
                    textWriter.Write(writer, new Text(sortedSentences.ToList()));

                    writer.WriteLine("\n");

                    writer.WriteLine("-> Distinct words from exclamatory sentences with length 3");
                    var distinctWords = text.GetDistinctWordsBy(x => x.Type == SentenceTypes.Exclamatory, x => x.Length == 3);
                    foreach (var word in distinctWords)
                    {
                        writer.WriteLine(word.StringValue.ToLower());
                    }

                    writer.WriteLine();

                    writer.WriteLine("-> In first sentence replace all words with length 5");
                    text.ReplaceWordsBy(6, "(it was replaced)", x => x.Length == 5, partsOfSentenceFactory);
                    textWriter.Write(writer, text);

                    writer.WriteLine("\n");

                    writer.WriteLine("-> Removed all words with length 6 and not stats with vowel");
                    text.RemoveWordsBy(x => !x.IsStartsWithVowel() && x.Length == 6);
                    textWriter.Write(writer, text);

                }
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
