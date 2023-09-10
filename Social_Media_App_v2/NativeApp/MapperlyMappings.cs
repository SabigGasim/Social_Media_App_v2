using Domain.Common;
using Domain.Entities;
using NativeApp.MVVM.Models;
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
    public static partial UserAlertDto Map(this UserAlertModel alert);
    public static partial UserAlertModel Map(this UserAlertDto alert);
    public static partial AccountAlertDto Map(this AccountAlertModel alert);
    public static partial AccountAlertModel Map(this AccountAlertDto alert);
    public static partial ReplyDto Map(this ReplyModel reply);
    public static partial ReplyModel Map(this ReplyDto reply);
    public static partial UserDto Map(this UserModel user);
    public static partial UserModel Map(this UserDto user);

    public static partial IEnumerable<PostDto> Map(this IEnumerable<PostModel> postCollection);
    public static partial IEnumerable<PostModel> Map(this IEnumerable<PostDto> postDtoList);
    public static partial IEnumerable<CommentModel> Map(this IEnumerable<CommentDto> commentDtos);
    public static partial IEnumerable<CommentDto> Map(this IEnumerable<CommentModel> commentDtos);
    public static partial IEnumerable<ReplyDto> Map(this IEnumerable<ReplyModel> replies);
    public static partial IEnumerable<ReplyModel> Map(this IEnumerable<ReplyDto> replies);
    public static partial IEnumerable<FollowRequestModel> Map(this IEnumerable<FollowRequestDto> followRequests);
    public static partial IEnumerable<FollowRequestDto> Map(this IEnumerable<FollowRequestModel> followRequests);
    public static partial IEnumerable<UserDto> Map(this IEnumerable<UserModel> user);
    public static partial IEnumerable<UserModel> Map(this IEnumerable<UserDto> user);
    
    public static IEnumerable<AlertModelBase> Map(this IEnumerable<AlertDtoBase> alerts)
    {
        return alerts.Select(alert =>
        {
            return alert is UserAlertDto
            ? (AlertModelBase)((UserAlertDto)alert).Map()
            : (AlertModelBase)((AccountAlertDto)alert).Map();
        });
    }
    
    public static IEnumerable<AlertDtoBase> Map(this IEnumerable<AlertModelBase> alerts)
    {
        return alerts.Select(alert =>
        {
            return alert is UserAlertModel
            ? (AlertDtoBase)((UserAlertModel)alert).Map()
            : (AlertDtoBase)((AccountAlertModel)alert).Map();
        });
    }
}

[Mapper(UseDeepCloning = true)]
public static partial class DeepCopyMapperlyMappings
{
    public static partial AccountInfoModel DeepCopy(this AccountInfoModel model);
    public static partial NotificationSettingsModel DeepCopy(this NotificationSettingsModel model);
    public static partial NotificationTypes DeepCopy(this NotificationTypes types);
    public static partial NotificationMethods DeepCopy(this NotificationMethods methods);
    public static partial PrivacyAndSecurityModel DeepCopy(this PrivacyAndSecurityModel model);
}
