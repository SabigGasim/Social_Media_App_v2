namespace Domain.Entities;
public class UserAlertDto : AlertDtoBase
{
    public UserDto User { get; set; }
    public MediaDto Thumbnail { get; set; }
}
