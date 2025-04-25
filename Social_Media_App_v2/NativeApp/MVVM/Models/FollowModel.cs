namespace NativeApp.MVVM.Models;

public class FollowModel
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public UserModel Follower { get; set; }
    public UserModel Following { get; set; }
}

