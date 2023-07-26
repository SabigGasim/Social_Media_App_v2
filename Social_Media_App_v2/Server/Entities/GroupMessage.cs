namespace Server.Entities;

public class GroupMessage : Message<GroupChat, GroupMessage>
{
    public IList<User> ReadFrom { get; set; }
}
