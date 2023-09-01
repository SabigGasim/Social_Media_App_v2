using System.Text.RegularExpressions;

namespace NativeApp.Validations.RegularExpressions;

/// <summary>
/// This regex is only responsible of matching alphabetical letters and numbers from many different languages.
/// It's not resposible of checking empty strings or white spaces.
/// </summary>
public partial class NicknameRegex
{
    private const string _nicknameRegex = @"^[\p{L}\p{N}_.\s\p{IsDevanagari}]*$";

    [GeneratedRegex(_nicknameRegex, RegexOptions.ECMAScript | RegexOptions.IgnoreCase)]
    private static partial Regex NicknameRegexGenerator();

    public static bool IsMatch(string input)
    {
        return NicknameRegexGenerator().IsMatch(input);
    }
}

