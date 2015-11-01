using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    public interface IParser<T,K>
    {
        K Parse(T input);
    }
}
