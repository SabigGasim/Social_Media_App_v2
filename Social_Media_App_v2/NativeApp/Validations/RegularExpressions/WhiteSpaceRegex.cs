using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NativeApp.Validations.RegularExpressions;
public static partial class WhiteSpaceRegex
{
    private const string _whiteSpaceRegex = @"\s+$";

    [GeneratedRegex(_whiteSpaceRegex)]
    private static partial Regex WhiteSpaceRegexGenerator();

    public static bool IsMatch(string input)
    {
        return WhiteSpaceRegexGenerator().IsMatch(input);
    }
}
