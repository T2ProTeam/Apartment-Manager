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
    internal class WrongNumber : ValidationRule
    {
        public string NameofRule { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.IsMatch(charString))
                return new ValidationResult(false, $"{NameofRule} Wrong !");
            return new ValidationResult(true, null);
        }
    }
}