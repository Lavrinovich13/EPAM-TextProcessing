using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Word : IWord
    {
        public Symbol[] Value
        {
            get { throw new NotImplementedException(); }
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
