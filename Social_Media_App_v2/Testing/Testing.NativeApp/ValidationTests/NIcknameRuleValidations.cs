using FluentAssertions;
using NativeApp.Validations.RegularExpressions;

namespace Testing.NativeApp.ValidationTests;

public class NicknameRuleValidationTests
{
    public static IEnumerable<object[]> ShouldMatch = NicknameValidationData.GetMatchingStrings();
    public static IEnumerable<object[]> ShouldFail = NicknameValidationData.GetFailingStrings();

    [Theory]
    [MemberData(nameof(ShouldMatch))]
    public void GivenValidInput_ShouldMatchRegex(string input)
    {
        NicknameRegex.IsMatch(input).Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ShouldFail))]
    public void GivenInvalidInput_ShouldNotMatchRegex(string input)
    {
        NicknameRegex.IsMatch(input).Should().BeFalse();
    }
}

file static class NicknameValidationData
{
    public static IEnumerable<object[]> GetMatchingStrings()
    {
        foreach(var str in shouldMatch)
        {
            yield return new object[] { str };
        }
    }

    public static IEnumerable<object[]> GetFailingStrings()
    {
        foreach (var str in shouldFail)
        {
            yield return new object[] { str };
        }
    }

    // Should match
    public static string[] shouldMatch = 
    {
        "     ",
        "Hello world",
        "مرحبا بالعالم 123",
        "こんにちは世界",
        "Hello_world",
        "مرحبا_عالم",
        "Hello.world",
        "مرحبا.عالم١٢٣",
        "مرحبا عالم",
        "English123",
        "中文456",
        "हिन्दी789",
        "Hello World.cs",
        "مرحبا عالم.java",
        "こんにちは世界.py 123٤٥٦",
        "Hello\nWorld",
        "english",
        "العربية",
        "русский",
        "english 123",
        "हिन्दी456",
        "中文789_",
        "\t\t\n",
        "\n",
        " prefix with space",
        "suffix with space ",
        "Spaces   extra",
        "공백이 너무 많아요",
    };
    
        // Should fail 
    public static string[] shouldFail = 
    {
        "Hello-World",
        "$Special*Ch@rs!",
        "#hashtag",
        "<html>",
        "British£pounds",
        "è특문!",
        "🌍Emoji",
        "~Tilde~",
        "?¿?¿",
        "%percent%",
        "!",
        "@",
        "#",
        "$",
        "%",
        "^",
        "&",
        "*",
        "(",
        ")",
        "-",
        "+",
        "=",
        "/",
        @"\",
        "|",
        "?",
        ":",
        ";",
        "'",
        "\"",
        "~",
        "`",
        "؛",
        "<",
        ">",
        "{",
        "}",
        "×",
        "÷",
        "‘",
    };
}
