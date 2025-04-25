using Humanizer;
using NativeApp.Interfaces;

namespace NativeApp.Validations.Rules;
public class OlderThanRule<T> : IValidationRule<T>
{
    private int _minAge;
    private string _propertyName = default!;

    public OlderThanRule(int minAge)
    {
        _minAge = minAge;
    }

    public string ValidationMessage { get; private set; } = default!;

    public string PropertyName
    {
        get => _propertyName;
        init
        {
            _propertyName = value;
            ValidationMessage = $"The minimum {_propertyName.Humanize(LetterCasing.LowerCase)} allowed is {_minAge}.";
        }
    }

    public bool Check(T? value)
    {
        var utcNow = DateTimeOffset.UtcNow;
        var minAge = utcNow.AddYears(-_minAge);

        DateOnly dateOfBirthDateOnly = value switch
        {
            DateOnly dateOnly => dateOnly,
            DateTimeOffset dateTimeOffset => DateOnly.FromDateTime(dateTimeOffset.DateTime),
            DateTime dateTime => DateOnly.FromDateTime(dateTime),
            _ => throw new ArgumentException("The specified argument is not a DateTime, DateTimeOffset or DateOnly")
        };

        var utcNowDateOnly = new DateOnly(utcNow.Year, utcNow.Month, utcNow.Day);
        var thirteenYearsAgo = new DateOnly(minAge.Year, minAge.Month, minAge.Day);

        return utcNowDateOnly > dateOfBirthDateOnly && thirteenYearsAgo >= dateOfBirthDateOnly;
    }
}
