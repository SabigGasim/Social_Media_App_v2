using System.Text.RegularExpressions;

namespace NativeApp.Validations.RegularExpressions;
public partial class UsernameRegex
{
    private const string _usernameRegex = @"^[a-zA-Z0-9_.]+$";

    [GeneratedRegex(_usernameRegex, RegexOptions.IgnoreCase)]
    private static partial Regex UsernameRegexGenerator();

    public static bool IsMatch(string input)
    {
        return UsernameRegexGenerator().IsMatch(input);
    }
}
