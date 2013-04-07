using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordStrength
{
    class Program
    {
        /// <summary>
        /// MAIN APPLICATION ENTRY POINT
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //SOMETHING TO COMPARE
            string pwd = "Password1";

            //TWO DIFFERENT SAMPLES TO SHOW THE PASSWORD MEETS THE REQUIREMENTS IF YOU ARE NOT LOOKING FOR A SPECIAL CHARACTER
            //BUT FAILS ONCE THE SPECIAL REQUIREMENT IS ADDED TO THE PARAMETER
            Console.WriteLine("{0}: {1}", pwd, ValidatePasswordComplexity(pwd, 6, 15, PasswordRules.Upper | PasswordRules.Lower | PasswordRules.Numeric));
            Console.WriteLine("{0}: {1}", pwd, ValidatePasswordComplexity(pwd, 6, 15, PasswordRules.Upper | PasswordRules.Lower | PasswordRules.Numeric | PasswordRules.Special));

            //WAIT FOR USER INPUT
            Console.ReadLine();
        }

        /// <summary>
        /// FUNCTION FOR TESTING PASSWORD COMPLEXITY
        /// </summary>
        /// <param name="s">INCOMING PASSWORD TO TEST</param>
        /// <param name="minLength">MIN LENGTH OF PASSWORD</param>
        /// <param name="maxLength">MAX LENGTH OF PASSWORD</param>
        /// <param name="rule">COMPARISON FLAGS CAN BE ANY COMBINATION OF THE FOLLOWING - UPPER, LOWER, NUMERIC, SPECIAL</param>
        /// <returns>TRUE OR FALSE IF THE PASSWORD PASSES OR NOT</returns>
        static bool ValidatePasswordComplexity(string s, int minLength, int maxLength, PasswordRules rule)
        {
            //CONSTANTS FOR COMPARISON GROUPS
            const string UPPER = "(?=.*[A-Z])";
            const string LOWER = "(?=.*[a-z])";
            const string NUMERIC = "(?=.*[0-9])";
            const string SPECIAL = "(?=.*[!@#$%^&*()<>])";

            //CREATE A NEW VAR TO HOLD THE COMPARE STRING WHILE ITS ASSEMBLED
            StringBuilder tmpCompare = new StringBuilder("^");

            //CHECK IF UPPER IS A REQUIREMENT
            if ((rule & PasswordRules.Upper) == PasswordRules.Upper)
                tmpCompare.Append(UPPER);

            //CHECK IF LOWER IS A REQUIREMENT
            if ((rule & PasswordRules.Lower) == PasswordRules.Lower)
                tmpCompare.Append(LOWER);

            //CHECK IF NUMERIC IS A REQUIREMENT
            if ((rule & PasswordRules.Numeric) == PasswordRules.Numeric)
                tmpCompare.Append(NUMERIC);

            //CHECK IF SPECIAL IS A REQUIREMENT
            if ((rule & PasswordRules.Special) == PasswordRules.Special)
                tmpCompare.Append(SPECIAL);

            //APPEND THE LENGTH REQUIREMENTS
            tmpCompare.Append(string.Format(".{{{0},{1}}}", minLength, maxLength));

            //RETURN THE RESULT OF THE COMPARISON
            return Regex.Match(s, tmpCompare.ToString()).Success;
        }

        /// <summary>
        /// ERNUMERATIONS FOR PASSWORD COMPLEXITY TESTING
        /// </summary>
        [Flags]
        public enum PasswordRules
        {
            Upper = 1,
            Lower = 2,
            Numeric = 4,
            Special = 8,
        }
    }
}
