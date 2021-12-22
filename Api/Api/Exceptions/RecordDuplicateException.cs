using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Exceptions
{
    public class RecordDuplicateException: Exception
    {
        public RecordDuplicateException() : base() { }

        public RecordDuplicateException(string message) : base(message) { }

        public RecordDuplicateException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
