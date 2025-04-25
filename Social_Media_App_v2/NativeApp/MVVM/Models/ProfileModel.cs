using System.Collections.ObjectModel;

namespace NativeApp.MVVM.Models;

public class ProfileModel
{
    public Guid Id { get; set; }
    public MediaModel Icon { get; set; }
    public string Description { get; set; }
}