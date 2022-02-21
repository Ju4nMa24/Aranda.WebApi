namespace Aranda.Common.Generics
{
    public static class Constants
    {
        /// <summary>
        /// Regular Expressions (Constants)
        /// </summary>
        public const string RegexWithSymbols = "^([0-9a-zA-Z@\n=()$%#°¿?!¡\"&*/|¬{}áÁ éÉ íÍ óÓ úÚ ñÑ,;_.'+-:]*)$";
        public const string PatternEmail = "^[_a-zA-Z0-9-]+(.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)*(.[a-zA-Z]{2,4})$";
        public const string PatternDigits = "^([0-9]*)$";
        public const string PatternPersonName = "^([a-zA-Z áÁ éÉ íÍ óÓ úÚ ñÑ,.'-]+)$";
        public const string PatternPhone = "^((\\+{0,1})([0-9]*))$";
        /// <summary>
        /// Default Values (Constants)
        /// </summary>
        public const string DefaultCode = "99";
        public const string DefaultMessage = "Se ha producido un error.";
        public const string HeaderErrorMessage = "ErrorMessage";
        public const string SuccessMessage = "SuccessMessage";
        /// <summary>
        /// Messages Types (Constants)
        /// </summary>
        public const string Code99 = "Code99";
        public const string Code01 = "Code01";
        public const string Code02 = "Code02";
        public const string Code00 = "Code00";
        public const string Code03 = "Code03";
    }
}