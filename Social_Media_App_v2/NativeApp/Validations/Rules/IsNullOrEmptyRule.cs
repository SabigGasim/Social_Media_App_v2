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
        switch (value)
        {
            case null:
                return false;
            
            case string str:
                return !string.IsNullOrEmpty(str);
            
            case IEquatable<T>:
                return !value.Equals(default);

            default:
                return !EqualityComparer<T>.Default.Equals(value, default);
        }
    }
}
