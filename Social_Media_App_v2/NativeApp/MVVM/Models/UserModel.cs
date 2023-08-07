﻿using Domain.Enums;
using System.Collections.ObjectModel;

namespace NativeApp.MVVM.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string Nickname { get; set; }
    public ProfileModel Profile { get; set; }
    public AccountState State { get; set; }
    public ObservableCollection<PostModel> Posts { get; set; }
}