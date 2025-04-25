﻿using Domain.Enums;

namespace NativeApp.MVVM.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string Nickname { get; set; }
    public long FollowersCount { get; set; }
    public long FollowingCount { get; set; }
    public long PostsCount { get; set; }
    public bool IsUserBeingFollowed { get; set; }
    public bool IsMuted { get; set; }
    public bool IsBlocked { get; set; }
    public ProfileModel Profile { get; set; }
    public AccountState State { get; set; }
    public Privacy AccountPrivacy { get; set; }
}