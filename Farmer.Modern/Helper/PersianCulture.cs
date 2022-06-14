using System.Collections.Generic;
using System.Globalization;

namespace Farmer.Modern.Helper
{
    public class PersianCulture : CultureInfo
    {
        private readonly Calendar _calendar;
        private readonly Calendar[] _optionalCalendars;
        private DateTimeFormatInfo _dateTimeFormatInfo;

        public PersianCulture() : base("fa-IR") {
            _calendar = new PersianCalendar();

            _optionalCalendars = new List<Calendar>
            {
                new PersianCalendar(),
                new GregorianCalendar()
            }.ToArray();

            var dateTimeFormatInfo = CultureInfo.CreateSpecificCulture("fa-IR").DateTimeFormat;
            dateTimeFormatInfo.Calendar = _calendar;
            _dateTimeFormatInfo = dateTimeFormatInfo;
        }

        public override Calendar Calendar => _calendar;

        public override Calendar[] OptionalCalendars => _optionalCalendars;

        public override DateTimeFormatInfo DateTimeFormat {
            get => _dateTimeFormatInfo;
            set => _dateTimeFormatInfo = value;
        }
    }
}