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
        SentenceTypes _Type { get; }
        int GetNumberOfWords();

        IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate);

        IEnumerable<IPartOfSentence> RemoveWordsBy(Func<IWord, bool> predicate);

        IEnumerable<IPartOfSentence> ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IPartOfSentence> parser);
    }
}
