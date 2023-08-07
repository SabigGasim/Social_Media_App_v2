using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApp.MVVM.Models;

#nullable enable

public sealed class PostModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; init; }
    public long Likes { get; set; }
    public long CommentsCount { get; set; }
    public ObservableCollection<MediaModel>? Media { get; set; }
}