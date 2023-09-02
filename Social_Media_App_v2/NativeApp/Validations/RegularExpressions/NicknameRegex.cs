using System.Text.RegularExpressions;

namespace NativeApp.Validations.RegularExpressions;

/// <summary>
/// This regex is responsible only for matching alphabetical letters and numbers from various languages.
/// It does not check for empty strings or white spaces.
/// </summary>
public static partial class NicknameRegex
{
    private const string _nicknameRegex = @"^[\p{L}\p{N}_.\s\p{IsDevanagari}]*$";

    [GeneratedRegex(_nicknameRegex, RegexOptions.ECMAScript | RegexOptions.IgnoreCase)]
    private static partial Regex NicknameRegexGenerator();

    public static bool IsMatch(string input)
    {
        return NicknameRegexGenerator().IsMatch(input);
    }
}

