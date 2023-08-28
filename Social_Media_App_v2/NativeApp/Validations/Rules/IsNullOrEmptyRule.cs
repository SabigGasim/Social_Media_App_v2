using Humanizer;
using NativeApp.Interfaces;

namespace NativeApp.Validations.Rules;
public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
{
    private string _propertyName = default!;

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"{_propertyName.Humanize(LetterCasing.Sentence)} cannot be empty.";
        }
    }

    public bool Check(T? value)
    {
        return value is string str && !string.IsNullOrEmpty(str);
    }
}
