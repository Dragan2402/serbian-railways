using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SerbianRailways.utility
{
    public class TimeSpanValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                TimeSpan time;
                if (TimeSpan.TryParse(s, out time))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Molimo vas unesite validno vreme.");
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška.");
            }
        }
    }

    public class MinMaxTimeSpanValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is TimeSpan)
            {
                TimeSpan Min = new TimeSpan(0, 0, 0, 0);
                TimeSpan Max = new TimeSpan(24, 0, 0, 0);
                TimeSpan d = (TimeSpan)value;
                if (d.CompareTo(Min) < 0) return new ValidationResult(false, "Molimo vas unesite validno vreme.");
                if (d.CompareTo(Max) > 0 ) return new ValidationResult(false, "Molimo vas unesite validno vreme.");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Nepoznata greška.");
            }
        }
    }
}
