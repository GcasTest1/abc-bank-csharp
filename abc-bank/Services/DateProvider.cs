using System;

namespace abc_bank.Services
{
    public class DateProvider
    {
        private static DateProvider instance = null;

        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
