using Humanizer;
using NativeApp.Interfaces;
using System.Collections;

namespace NativeApp.Validations.Rules;
public class IsNotEqualToRule<T> : IValidationRule<T>
{
    private T? _value;
    private string _propertyName = default!;

    public IsNotEqualToRule(T value)
    {
        _value = value;
    }

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"Invalid value for {_propertyName.Humanize(LetterCasing.Sentence)}";
        }
    }

    public bool Check(T? value)
    {
        return value switch
        {
            IEquatable<T> equatable => !equatable.Equals(_value),
            IStructuralEquatable equatable => !equatable.Equals(_value),
            _ => !EqualityComparer<T>.Default.Equals(_value, value)
        };
    }
}
