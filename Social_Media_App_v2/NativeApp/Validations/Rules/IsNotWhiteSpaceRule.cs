using Humanizer;
using NativeApp.Interfaces;
using NativeApp.Validations.RegularExpressions;

namespace NativeApp.Validations.Rules;
public class IsNotWhiteSpaceRule<T> : IValidationRule<T>
{
    private string _propertyName = default!;

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"{_propertyName.Humanize(LetterCasing.Sentence)} cannot be a whitespace";
        }
    }
    public bool Check(T? value)
    {
        return value is string str && !WhiteSpaceRegex.IsMatch(str);
    }
}
