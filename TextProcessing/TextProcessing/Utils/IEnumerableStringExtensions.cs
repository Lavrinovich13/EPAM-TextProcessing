using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public static class IEnumerableStringExtensions
    {
        public static string ToRegexStringWithoutRepetitions(this IEnumerable<string> collection)
        {
            var stringWithoutRepetitions =
                            collection.SerialConcatBy(x => true, null)
                                      .ElementAt(0).Distinct()
                                      .Select(x => "\\" + (x.ToString()))
                                      .SerialConcatBy(x => true,null)
                                      .ElementAt(0);
            return stringWithoutRepetitions;
        }
        public static IEnumerable<string> SerialConcatBy
            (this IEnumerable<string> collection, Func<string, bool> function, string concatString = " ")
        {
            var parts = new List<string>();
            string lastPart = null;
            foreach (var item in collection)
            {
                if (function(item))
                {
                    lastPart += concatString + item;
                }
                else
                {
                    if (lastPart != null)
                    {
                        parts.Add(lastPart);
                    }
                    lastPart = item;
                }
            }
            parts.Add(lastPart);
            return parts.Where(x => !String.IsNullOrEmpty(x)).ToList();
        }
    }
}
