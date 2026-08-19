using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(value) + "/" + calendar.GetMonth(value).ToString("00") + "/" + 
                calendar.GetDayOfMonth(value).ToString("00");
        }
    }
}
