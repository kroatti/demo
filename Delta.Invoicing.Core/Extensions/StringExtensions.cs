namespace Delta.Invoicing.Core.Extensions
{
    public static class StringExtensions
    {
        public static string? Nullify(this string? str, bool removeWhiteSpace = false)
        {
            if (removeWhiteSpace) str = str?.Trim();
            return str == string.Empty ? null : str;
        }
    }
}
