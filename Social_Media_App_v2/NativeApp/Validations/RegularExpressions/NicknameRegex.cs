using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NativeApp.Validations.RegularExpressions;
public partial class NicknameRegex
{
    private const string _nicknameRegex = @"^[a-zA-Z0-9_.\s*]+$";

    [GeneratedRegex(_nicknameRegex, RegexOptions.IgnoreCase)]
    private static partial Regex NicknameRegexGenerator();

    public static bool IsMatch(string input)
    {
        return NicknameRegexGenerator().IsMatch(input);
    }
}

