using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Exceptions
{
    public class RecordNotFoundException: Exception
    {
        public RecordNotFoundException() : base() { }

        public RecordNotFoundException(string message) : base(message) { }

        public RecordNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
