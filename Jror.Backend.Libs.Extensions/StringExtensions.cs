using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Jror.Backend.Libs.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns an array converted into a strings.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ArrayToString(this Array input, string separator)
        {
            var ret = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                ret.Append(input.GetValue(i));
                if (i != input.Length - 1)
                    ret.Append(separator);
            }
            return ret.ToString();
        }

        /// <summary>
        /// Capitalizes a word or sentence.
        /// Ex.: this is a sentence -> This is a sentence.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Capitalize(string input)
        {
            if (input.Length == 0) return string.Empty;
            if (input.Length == 1) return input.ToUpper();

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Checks if the string contains numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasNumbers(this string input)
        {
            return Regex.IsMatch(input, "\\d+");
        }

        /// <summary>
        /// Checks to see if a string contains vowels.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasVowels(this string input)
        {
            string currentLetter;
            for (var i = 0; i < input.Length; i++)
            {
                currentLetter = input.Substring(i, 1);

                if (string.Compare(currentLetter, "a", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "e", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "i", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "o", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(currentLetter, "u", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    //A vowel found
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if string is numbers and letters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphaNumberic(this string input)
        {
            char currentLetter;
            for (var i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];

                if (!(Convert.ToInt32(currentLetter) >= 48 && Convert.ToInt32(currentLetter) <= 57) &&
                    !(Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 90) &&
                    !(Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122))
                {
                    //Not a number or a letter
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if string has only numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (!(Convert.ToInt32(input[i]) >= 48 && Convert.ToInt32(input[i]) <= 57))
                {
                    //Not integer value
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether a string is in all upper case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUpperCase(this string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (string.CompareOrdinal(input.Substring(i, 1), input.Substring(i, 1).ToUpper()) != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Converts an string to a Guid. This could be used within a unit test to mock objects.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            Guid.TryParse(value, out Guid result);

            return result;
        }
    }
}