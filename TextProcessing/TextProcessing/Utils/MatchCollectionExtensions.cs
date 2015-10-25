using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessing
{
    public static class MatchCollectionExtensions
    {
        public static IEnumerable<string> AsEnumerable(this MatchCollection collection, int numGroups)
        {
            //TODO maybe we must pass a number of groups
            foreach (Match item in collection)
            {
                for (int i = 1; i <= numGroups; i++)
                {
                    var value = item.Groups[i].ToString();
                    if (!String.IsNullOrWhiteSpace(value))
                    {
                        yield return value;
                    }
                }
            }
        }
    }
}
