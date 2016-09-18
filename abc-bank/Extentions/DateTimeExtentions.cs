using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Extentions
{
    public static class DateTimeExtentions
    {
        public static int GetDaysInTheYear(this DateTime date)
        {
            var year = date.Year;
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }
    }
}
