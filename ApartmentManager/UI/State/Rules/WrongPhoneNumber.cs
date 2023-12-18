﻿using Microsoft.Extensions.FileSystemGlobbing.Internal;
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
    public class WrongPhoneNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex(@"^0[0-9]{10}$");
            if (!regex.IsMatch(charString))
                return new ValidationResult(false, $"Phone Number Wrong !(Length Phone Number must have 11 number");
            return new ValidationResult(true, null);
        }
    }
}