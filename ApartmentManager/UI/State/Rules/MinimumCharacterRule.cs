using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AM.UI.State.Rules
{
    public class MinimumCharacterRule : ValidationRule
    {
        public int MinimumCharacters { get; set; }
        public int Characters { get; set; }
        public string nameofrule { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            
               
            
            if (Characters==1)
            {
                if (charString.Length < MinimumCharacters)
                    return new ValidationResult(false, $"{nameofrule} atleast {MinimumCharacters} characters.");
            }
            if (Characters==0)
            {
                if (charString.Length == MinimumCharacters)
                    return new ValidationResult(false, $"{nameofrule} must compare  {MinimumCharacters} characters.");
            }
            if (charString.Length > MinimumCharacters)
                return new ValidationResult(false, $"{nameofrule}  under {MinimumCharacters} characters.");
            return new ValidationResult(true, null);
        }
    }
}