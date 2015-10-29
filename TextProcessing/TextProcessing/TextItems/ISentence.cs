using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    interface ISentence
    {
        IEnumerable<IPartOfSentence> _Items { get; }
        int GetNumberOfWords();

        IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate);

        IEnumerable<IWord> RemoveWordsBy(Func<IWord, bool> predicate);

        IEnumerable<IWord> ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IPartOfSentence> parser);
    }
}
