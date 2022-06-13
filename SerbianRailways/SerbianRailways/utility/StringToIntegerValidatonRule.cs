using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SerbianRailways.utility
{
    public class StringToIntegerValidatonRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (int.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Molimo vas unesite pozitivan ceo broj za dato polje.");
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška.");
            }
        }
    }

    public class MinMaxValidationIntegerRule : ValidationRule
    {
        public int Min
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int d = (int)value;
                if (d < Min) return new ValidationResult(false, "Broj mora biti ceo pozitivan.");
                if (d > Max) return new ValidationResult(false, "Broj ne može biti veći od "+Max.ToString()+".");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Nepoznata greška.");
            }
        }
    }
}
