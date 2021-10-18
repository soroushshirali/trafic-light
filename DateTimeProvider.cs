using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traffic_light
{
    public class DateTimeProvider
    {
        private readonly DateTime? _dateTime = null;

        public DateTimeProvider()
        {

        }

        public DateTimeProvider(DateTime fixedDateTime)
            => _dateTime = fixedDateTime;

        public DateTime Now => _dateTime ?? DateTime.Now;
    }
}
