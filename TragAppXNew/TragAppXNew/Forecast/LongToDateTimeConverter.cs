using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace TragAppXNew.Forecast
{
    class LongToDateTimeConverter
    {

        DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long dateTime = (long)value;
            return $"{_time.AddSeconds(dateTime).ToString()} UTC";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}