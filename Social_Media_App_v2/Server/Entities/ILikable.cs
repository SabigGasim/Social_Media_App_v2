namespace Server.Entities;

public interface ILikable
{
    long Likes { get; set; }
    IList<User> LikedFrom { get; set; }
}
