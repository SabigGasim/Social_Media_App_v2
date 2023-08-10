using Domain.Enums;

namespace NativeApp.MVVM.Models;

public class FollowRequestModel
{
    public Guid Id { get; set; }
    public FollowRequestState State { get; set; }
    public UserModel Follower { get; set; }
    public UserModel Following { get; set; }
}
