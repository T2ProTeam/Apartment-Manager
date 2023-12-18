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
    internal class ContainsSpecialCharacter : ValidationRule
    {
        public string NameOfRule { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"[^a-zA-Z0-9\s]+$");
            if (regex.IsMatch(charString))
                return new ValidationResult(false, $"{NameOfRule} Wrong !)");
            return new ValidationResult(true, null);
        }
    }
}