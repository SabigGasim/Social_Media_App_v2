using Humanizer;
using NativeApp.Interfaces;
using NativeApp.Validations.RegularExpressions;

namespace NativeApp.Validations.Rules;
public class EmailRule<T> : IValidationRule<T>
{
    private string _propertyName = default!;

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"Invalid {_propertyName.Humanize(LetterCasing.LowerCase)} format.";
        }
    }

    public bool Check(T? value)
    {
        return value is string str && EmailRegex.IsMatch(str);
    }
}
