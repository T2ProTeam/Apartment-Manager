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
    public class WrongEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$");
            if (!regex.IsMatch(charString))
                return new ValidationResult(false, $"Email Wrong !");
            return new ValidationResult(true, null);
        }
    }
}