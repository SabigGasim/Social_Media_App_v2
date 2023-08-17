namespace NativeApp.MVVM.Models;

public class AccountInfoModel
{
    public UserModel User { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
