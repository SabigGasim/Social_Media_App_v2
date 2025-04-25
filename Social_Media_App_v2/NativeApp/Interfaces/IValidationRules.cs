namespace NativeApp.Interfaces;

public interface IValidationRule<T>
{
    string ValidationMessage { get; }
    string PropertyName { get; init;  }
    bool Check(T? value);
}
