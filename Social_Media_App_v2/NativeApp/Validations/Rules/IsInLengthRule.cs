using Humanizer;
using NativeApp.Interfaces;

namespace NativeApp.Validations.Rules;
public class IsInLengthRule<T> : IValidationRule<T>
{
    private int _min;
    private int _max;
    private string _propertyName = default!;

    public IsInLengthRule(int length)
    {
        _min = 0;
        _max = length;
    }

    public IsInLengthRule(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"{_propertyName.Humanize(LetterCasing.Sentence)} length should be in range {_min}-{_max} characters.";
        }
    }

    public bool Check(T? value)
    {
        return value is string str && str.Length >= _min && str.Length <= _max;
    }
}
