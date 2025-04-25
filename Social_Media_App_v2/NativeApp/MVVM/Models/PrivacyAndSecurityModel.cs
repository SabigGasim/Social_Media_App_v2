using Domain.Enums;

namespace NativeApp.MVVM.Models;

public class PrivacyAndSecurityModel
{
    public Privacy Privacy { get; set; }
    public bool TwoFactorEnabled { get; set; }
}