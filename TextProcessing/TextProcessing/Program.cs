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

            IFactory<string, IPartOfSentence> partsOfSentenceFactory
                = new PartsOfSentenceFactory(defaultDelimeters);

            Parser textParser = new Parser(defaultDelimeters, partsOfSentenceFactory);

            Text text = textParser.Parse(new StreamReader("text1.txt"));

            var sortedSentences = text.GetSentences().OrderBy(x => x.GetNumberOfWords());
            
            TextStreamWriter textWriter = new TextStreamWriter();
            textWriter.Write(new StreamWriter("result1.txt"), new Text(sortedSentences.ToList()));
        }
    }
}
