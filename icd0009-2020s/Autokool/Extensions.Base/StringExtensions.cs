namespace Extensions.Base
{
    public static class StringExtensions
    {
        public static string LimitStringToMaxLength(this string str, int maxLength)
        {
            if (str.Length > (maxLength-3))
            {
                return str.Substring(0, maxLength-3) + "...";
            }

            return str;
        }
    }
}