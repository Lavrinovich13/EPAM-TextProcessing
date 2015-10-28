using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    class Delimeter
    {
        public string _StringValue { get; private set; }
        public bool _IsConnectedWithSpace { get; private set; }
        public bool _IsPairDelimeter { get; private set; }

        public Delimeter(string value, bool isConnectedWithSpace, bool isPairDelimeter)
        {
            this._StringValue = value;
            this._IsConnectedWithSpace = isConnectedWithSpace;
            this._IsPairDelimeter = isPairDelimeter;
        }
    }
}
