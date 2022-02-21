using System.Text.RegularExpressions;

namespace Aranda.Common.Generics
{
    /// <summary>
    /// class with regular expressions.
    /// </summary>
    public static class RegularExpressions
    {
        public readonly static Regex RegexWithSymbol = new Regex(Constants.RegexWithSymbols);
        public readonly static Regex RegexEmail = new Regex(Constants.PatternEmail, RegexOptions.IgnoreCase);
        public readonly static Regex RegexDigits = new Regex(Constants.PatternDigits);
        public readonly static Regex RegexPersonName = new Regex(Constants.PatternPersonName);
        public readonly static Regex RegexPhone = new Regex(Constants.PatternPhone);
    }
}
