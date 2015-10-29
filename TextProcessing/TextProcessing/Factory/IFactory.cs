using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    interface IFactory<T, K>
    {
        IEnumerable<K> Build(T input);
    }
}
