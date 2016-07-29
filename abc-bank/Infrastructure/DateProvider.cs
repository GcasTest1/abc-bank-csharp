using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Contract;

namespace abc_bank.Infrastructure
{
    public sealed class DateProvider
    {
        private static readonly Lazy<DateProvider> instance = new Lazy<DateProvider>(() => new DateProvider());

        private DateProvider() { }

        public static DateProvider getInstance()
        {
            return instance.Value;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
