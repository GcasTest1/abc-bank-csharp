using System;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider _instance;

        public static DateProvider GetInstance()
        {
            return _instance ?? (_instance = new DateProvider());
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
