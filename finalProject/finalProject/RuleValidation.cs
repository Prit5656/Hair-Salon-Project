using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace finalProject
{
    public class RuleValidation : ValidationRule
    {
        private int minValue;
        private int maxValue;
        private string phone;

        public int MinimumValue { get => minValue; set => minValue = value; }
        public int MaximumValue { get => maxValue; set => maxValue = value; }
        public string Phone { get => phone; set => phone = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int tempValidationNumber = 0;
            if (!int.TryParse(value.ToString(), out tempValidationNumber))
            {
                return new ValidationResult(false, "The input should be a number");
            }

            if (tempValidationNumber < minValue || tempValidationNumber > maxValue)
            {
                return new ValidationResult(false, "This number is not in Range");
            }

            if (value.ToString() == "phone") {
                return new ValidationResult(false, "The number should be in 10 digits");
            }

            
            return ValidationResult.ValidResult;
        }
    }
}
