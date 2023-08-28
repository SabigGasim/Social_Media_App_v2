using Humanizer;
using NativeApp.Interfaces;
using NativeApp.Validations.RegularExpressions;

namespace NativeApp.Validations.Rules;
public class UsernameRule<T> : IValidationRule<T>
{
    private string _propertyName = default!;

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"{_propertyName.Humanize(LetterCasing.Sentence)} only accepts a-z, 0-9, underscores and dots.";
        }
    }

    public bool Check(T? value)
    {
        return value is string username && UsernameRegex.IsMatch(username);
    }
}
