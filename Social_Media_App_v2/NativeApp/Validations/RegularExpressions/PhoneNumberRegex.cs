using System.Text.RegularExpressions;

namespace NativeApp.Validations.RegularExpressions;
public static partial class PhoneNumberRegex
{
    private const string _regex = "^\\+?[1-9][0-9]{7,14}$";

    [GeneratedRegex(_regex)]
    private static partial Regex PhoneNumberRegexGenerator();

    public static bool IsMatch(string input)
    {
        return PhoneNumberRegexGenerator().IsMatch(input);
    }
}
