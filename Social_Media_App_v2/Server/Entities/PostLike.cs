﻿namespace Server.Entities;

public class PostLike
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}
