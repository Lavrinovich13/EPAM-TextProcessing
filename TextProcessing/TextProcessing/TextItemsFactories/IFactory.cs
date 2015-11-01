using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    public interface IFactory<T, K>
    {
        K Construct(T input);
    }
}
