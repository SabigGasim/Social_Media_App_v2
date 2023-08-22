namespace NativeApp.MVVM.Models;

public class AccountInfoModel
{
    public string Username { get; set; } = default!;
    public string Nickname { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
