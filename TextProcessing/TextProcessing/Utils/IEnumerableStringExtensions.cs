using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public static class IEnumerableStringExtensions
    {
        public static string ToStringWithoutCharRepetitions(this IEnumerable<string> collection)
        {
            var stringWithoutRepetitions =
                            collection.SerialConcatBy(x => true, null)
                                      .ElementAt(0).Distinct()
                                      .Select(x => x.ToString())
                                      .SerialConcatBy(x => true,null)
                                      .ElementAt(0);
            return stringWithoutRepetitions;
        }
        public static IEnumerable<string> SerialConcatBy(this IEnumerable<string> collection, Func<string, bool> function, string concatString = " ")
        {
            var sentences = new List<string>();
            string lastSentence = null;
            foreach (var item in collection)
            {
                if (function(item))
                {
                    lastSentence += concatString + item;
                }
                else
                {
                    if (lastSentence != null)
                    {
                        sentences.Add(lastSentence);
                    }
                    lastSentence = item;
                }
            }
            sentences.Add(lastSentence);
            return sentences.Where(x => !String.IsNullOrEmpty(x)).ToList();
        }
    }
}
