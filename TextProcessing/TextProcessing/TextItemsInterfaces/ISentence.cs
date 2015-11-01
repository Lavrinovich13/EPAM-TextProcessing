using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public interface ISentence
    {
        IEnumerable<IPartOfSentence> Items { get; }

        SentenceTypes Type { get; }

        int GetNumberOfWords();

        IEnumerable<IWord> GetWordsBy(Func<IWord, bool> predicate);

        void RemoveWordsBy(Func<IWord, bool> predicate);

        void ReplaceWordsBy
            (Func<IWord, bool> predicate, string replaceString, IFactory<string, IEnumerable<IPartOfSentence>> parser);
    }
}
