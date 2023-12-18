using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AM.UI.State.Rules
{
    public class WrongName : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^[a-zA-Z\s]+$");
            if (!regex.IsMatch(charString))
                return new ValidationResult(false, $"Name Wrong !");
            return new ValidationResult(true, null);
        }
    }
}