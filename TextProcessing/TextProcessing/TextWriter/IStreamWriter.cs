using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    public interface IStreamWriter<K>
    {
        void Write(StreamWriter stream, K input);
    }
}
