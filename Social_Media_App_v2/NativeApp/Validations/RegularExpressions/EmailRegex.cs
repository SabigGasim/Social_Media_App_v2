using System.Text.RegularExpressions;

namespace NativeApp.Validations.RegularExpressions;
public partial class EmailRegex
{
    private const string _emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    [GeneratedRegex(_emailRegex, RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegexGenerator();

    public static bool IsMatch(string input)
    {
        return EmailRegexGenerator().IsMatch(input);
    }
}
