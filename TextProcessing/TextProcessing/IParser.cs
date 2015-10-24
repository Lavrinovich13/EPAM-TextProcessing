using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    interface IParser<T,K>
    {
        T Parse(K input);
    }
}
