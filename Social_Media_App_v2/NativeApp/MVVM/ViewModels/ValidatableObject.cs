using NativeApp.Interfaces;

namespace NativeApp.MVVM.ViewModels;
public class ValidatableObject<T> : ViewModelBase
{
    private IEnumerable<string>? _errors;
    private bool _isValid;
    private T? _value;
    public List<IValidationRule<T>>? Validations { get; } = new();
    
    public ValidatableObject()
    {
        _isValid = true;
        _errors = Enumerable.Empty<string>();
    }

    public IEnumerable<string>? Errors
    {
        get => _errors;
        private set => TrySetValue(ref _errors, value);
    }
    public bool IsValid
    {
        get => _isValid;
        private set => TrySetValue(ref _isValid, value);
    }
    public T? Value
    {
        get => _value;
        set => TrySetValue(ref _value, value);
    }

    public bool Validate()
    {
        Errors = Validations
            ?.Where(v => !v.Check(Value))
            ?.Select(v => v.ValidationMessage)
            ?? Enumerable.Empty<string>();

        IsValid = !Errors.Any();
        return IsValid;
    }
}
