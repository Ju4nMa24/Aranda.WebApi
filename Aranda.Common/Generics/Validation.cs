using System;

namespace Aranda.Common.Generics
{
    /// <summary>
    /// Class for field validations
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Guid validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateGuid(string value)
        {
            if (value == null) return false;
            return Guid.TryParse(value, out var _);
        }
        /// <summary>
        /// Guid validation and nulls.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateGuidAndNull(string value)
        {
            if (value == null) return true;
            return Guid.TryParse(value, out var _);
        }
        /// <summary>
        /// Numbers Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateNumberAndNull(string value) => (value is not null && RegularExpressions.RegexPhone.IsMatch(value));
        /// <summary>
        /// Boolean Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateBoolAndNull(string value)
        {
            if (value == null) return true;
            return bool.TryParse(value, out var _);
        }
        /// <summary>
        /// Boolean Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateBool(string value)
        {
            if (value == null) return false;
            return bool.TryParse(value, out var _);
        }
        /// <summary>
        /// String Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateStringAndNull(string value) => (value is null || string.IsNullOrEmpty(value.Trim()));
        /// <summary>
        /// Regex with Symbol Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRegexWithSymbolAndNull(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return RegularExpressions.RegexWithSymbol.IsMatch(value);
        }
        /// <summary>
        /// Regex with Symbol Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRegexWithRegexPersonNameAndNull(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return RegularExpressions.RegexPersonName.IsMatch(value);
        }
        /// <summary>
        /// Number Validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateNumber(string value)
        {
            if (value == null) return false;
            return int.TryParse(value, out var _);
        }
        public static bool ValidateNumberCompostType(string value)
        {
            if (value == null) return false;
            if (value != "1" && value != "2") return false;
            return int.TryParse(value, out var _);
        }
        /// <summary>
        /// Validation of lengths of numbers.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateLongNumber(string value)
        {
            if (value == null) return false;
            return long.TryParse(value, out var _);
        }
        /// <summary>
        /// Emails validation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateEmail(string value) => (RegularExpressions.RegexEmail.IsMatch(value));
    }
}
