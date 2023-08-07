﻿using Domain.Entities;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels;
using Riok.Mapperly.Abstractions;

namespace NativeApp;

[Mapper]
public static partial class MapperlyMappings
{
    public static partial PostModel Map(this PostDto post);
    public static partial PostDto Map(this PostModel postModel);

    [MapProperty(nameof(CommentDto.CreatedDate), nameof(CommentModel.Date))]
    public static partial CommentModel Map(this CommentDto comment);
    [MapProperty(nameof(CommentModel.Date), nameof(CommentDto.CreatedDate))]
    public static partial CommentDto Map(this CommentModel comment);

    private static PostDto MapPostViewModeltoPostDto(this PostViewModel? postViewModel)
    {
        return Map(postViewModel?.Post!);
    }


    public static partial IEnumerable<PostDto> Map(this ObservableList<PostModel> postCollection);
    public static partial IEnumerable<PostDto> Map(this ObservableList<PostViewModel> postCollection);
    
    public static partial IEnumerable<PostDto> Map(this IEnumerable<PostModel> postCollection);

    public static partial IEnumerable<PostModel> Map(this IEnumerable<PostDto> postDtoList);

    public static partial IEnumerable<CommentModel> Map(this IEnumerable<CommentDto> commentDtos);
    public static partial IEnumerable<CommentDto> Map(this IEnumerable<CommentModel> commentDtos);
}