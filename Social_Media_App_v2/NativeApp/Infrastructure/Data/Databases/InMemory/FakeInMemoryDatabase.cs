using Bogus;
using Domain.Enums;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;

namespace NativeApp.Infrastructure.Data.Databases.InMemory;
public class FakeInMemoryDatabase : IDatabase
{
    private readonly List<PostModel> _posts;
    private readonly List<CommentModel> _comments;
    private readonly List<ReplyModel> _replies;
    private readonly List<UserModel> _users;
    private readonly List<FollowRequestModel> _followRequests;

    public FakeInMemoryDatabase()
    {
        _comments = new();
        _replies = new();
        _users = new();
        _posts = new();
        _followRequests = new();

        SetFakeDatabase(100);
    }

    public Task<IEnumerable<PostModel>> GetTimelinePosts(Guid? lastSeenPostId, int numberOfPosts)
    {
        if (lastSeenPostId is null)
        {
            return Task.FromResult(_posts.Take(Math.Min(numberOfPosts, _posts.Count)));
        }

        var remaining = _posts
            .SkipWhile(post => post.Id != lastSeenPostId)
            .Skip(1);

        var postsToTake = Math.Min(numberOfPosts, remaining.Count());

        return Task.FromResult(remaining.Take(postsToTake));
    }

    public Task AddTimeLinePosts(IEnumerable<PostModel> posts)
    {
        if (posts is null || posts.Count() == 0)
        {
            SetFakeDatabase(10);
            return Task.CompletedTask;
        }

        _posts.AddRange(posts);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<CommentModel>> GetPostComments(Guid? lastSeenCommentId, Guid? postId, int numberOfComments)
    {
        if (lastSeenCommentId is null)
        {
            var comments = _comments.Where(x => x.PostId == postId);

            var count = Math.Min(numberOfComments, comments.Count());

            return Task.FromResult(comments.Take(count));
        }

        var remaining = _comments
            .Where(x => x.PostId == postId)
            .SkipWhile(comment => comment.Id != lastSeenCommentId)
            .Skip(1);


        var commentsToTake = Math.Min(numberOfComments, remaining.Count());

        return Task.FromResult(remaining.Take(commentsToTake));
    }

    public Task<IEnumerable<ReplyModel>> GetCommentReplies(Guid commentId, Guid? lastSeenReplyId, int numberOfReplies)
    {
        var replies = _replies.Where(x => x.CommentId == commentId);

        if (lastSeenReplyId is null)
        {
            var count = Math.Min(numberOfReplies, replies.Count());

            return Task.FromResult(replies.Take(count));
        }

        var remaining = replies
            .SkipWhile(reply => reply.Id != lastSeenReplyId)
            .Skip(1);

        var repliesToTake = Math.Min(numberOfReplies, remaining.Count());

        return Task.FromResult(remaining.Take(repliesToTake));
    }

    public Task<IEnumerable<UserModel>> FindUsersByUsername(string query)
    {
        query = query.ToLower();
        var users = _users.Where(user => user.Nickname.ToLower()!.StartsWith(query) || user.UserName.ToLower().StartsWith(query));

        return Task.FromResult(users);
    }

    private void SetFakeDatabase(int numberOfPosts)
    {
        var mediaFaker = GetMediaFaker();
        var userFaker = GetUserFaker(mediaFaker);
        var replyFaker = GetReplyFaker(userFaker);
        var commentsFaker = GetCommentsFaker(userFaker);
        var postFaker = GetPostsFaker(userFaker, mediaFaker);

        _posts.AddRange(postFaker.Generate(numberOfPosts));

        foreach(var post in _posts)
        {
            var comments = commentsFaker.Generate((int)post.CommentsCount)
                .Select(x =>
                {
                    x.PostId = post.Id;
                    return x;
                });

            _comments.AddRange(comments);

            foreach (var comment in comments)
            {

                _replies.AddRange(replyFaker.Generate(comment.RepliesCount)
                    .Select(x =>
                    {
                        x.CommentId = comment.Id;
                        x.ReplyingTo = comment.User;

                        return x;
                    }));
            }
        }

        var users = _posts.Select(post => post.User)
            .Concat(_comments.Select(comment => comment.User))
            .Concat(_replies.Select(reply => reply.User))
            .Distinct();

        _users.AddRange(users);

        var followRequestsFaker = GetFollowRequestsFaker(_users);

        _followRequests.AddRange(followRequestsFaker!.Generate(_users.Count / 2));
    }

    public Task<IEnumerable<FollowRequestModel>> GetFollowRequests(Guid? userId, Guid? lastSeenFollowRequest, int numberOfRequests)
    {
        userId ??= _users.First().Id;

        var requests = _followRequests.Where(req => req.Following.Id == userId);

        if (lastSeenFollowRequest is null)
        {
            var count = Math.Min(numberOfRequests, requests.Count());

            return Task.FromResult(requests.Take(count));
        }

        var remaining = requests
            .SkipWhile(req => req.Id != lastSeenFollowRequest)
            .Skip(1);

        var repliesToTake = Math.Min(numberOfRequests, remaining.Count());

        return Task.FromResult(remaining.Take(repliesToTake));
    }

    private static Faker<PostModel> GetPostsFaker(Faker<UserModel> userFaker, Faker<MediaModel> mediaFaker)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());

        return new Faker<PostModel>()
            .RuleFor(x => x.User, () => userFaker.Generate())
            .RuleFor(x => x.Media, () =>
            {
                var postMediaFilesLength = randomizer.Next(-4, 5);

                if (postMediaFilesLength <= 0)
                {
                    return new();
                }

                return new(mediaFaker.Generate(postMediaFilesLength));
            })
            .RuleFor(x => x.Text, x => x.Lorem.Text())
            .RuleFor(x => x.Likes, () => randomizer.Next(100, 3_000_000))
            .RuleFor(x => x.CommentsCount, () => randomizer.Next(0, 30))
            .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now))
            .RuleFor(x => x.Id, (x) => Guid.NewGuid());
    }

    private static Faker<MediaModel> GetMediaFaker()
    {
        return new Faker<MediaModel>()
            .RuleFor(x => x.Url, x => x.Image.LoremFlickrUrl(1920, 1080));
    }

    private static Faker<CommentModel> GetCommentsFaker(Faker<UserModel> fakeUser)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        return new Faker<CommentModel>()
            .RuleFor(comment => comment.Text, f => f.Lorem.Text())
            .RuleFor(comment => comment.User, () => fakeUser.Generate())
            .RuleFor(comment => comment.Date, f => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
            .RuleFor(comment => comment.Likes, () => randomizer.Next(100_000))
            .RuleFor(comment => comment.RepliesCount, () => randomizer.Next(30))
            .RuleFor(comment => comment.Id, () => Guid.NewGuid());
    }

    private static Faker<ReplyModel> GetReplyFaker(Faker<UserModel> fakeUser)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        return new Faker<ReplyModel>()
            .RuleFor(reply => reply.User, () => fakeUser.Generate())
            .RuleFor(reply => reply.Text, f => f.Lorem.Text())
            .RuleFor(reply => reply.Likes, () => randomizer.Next(10_000))
            .RuleFor(reply => reply.Id, Guid.NewGuid)
            .RuleFor(reply => reply.Date, f => f.Date.Past(3));
    }

    private static Faker<UserModel> GetUserFaker(Faker<MediaModel> mediaFaker)
    {
        var profileFaker = GetProfileFaker(mediaFaker);

        var fakeUser = new Faker<UserModel>()
                .RuleFor(user => user.Nickname, f => f.Name.FullName())
                .RuleFor(user => user.UserName, f => f.Name.FirstName())
                .RuleFor(user => user.Profile, () => profileFaker.Generate());
        return fakeUser;
    }

    private static Faker<ProfileModel> GetProfileFaker(Faker<MediaModel> mediaFaker)
    {
        var profileFaker = new Faker<ProfileModel>()
                .RuleFor(profile => profile.Icon, () => mediaFaker.Generate());

        return profileFaker;
    }

    private static Faker<FollowRequestModel> GetFollowRequestsFaker(IList<UserModel> users)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        var followerIndices = Enumerable.Range(0, users.Count).ToList();
        var followingIndices = Enumerable.Range(0, users.Count).ToList();

        return new Faker<FollowRequestModel>()
            .RuleFor(req => req.Following, () =>
            {
                var index = randomizer.Next(0, followingIndices.Count);
                var user = users[index]!;
                followingIndices.RemoveAt(index);
                return user;
            })
            .RuleFor(req => req.Follower, () =>
            {
                var index = randomizer.Next(0, followerIndices.Count);
                var user = users[index]!;
                followerIndices.RemoveAt(index);
                return user;
            })
            .RuleFor(req => req.State, FollowRequestState.Pending)
            .RuleFor(req => req.Id, () => Guid.NewGuid());
    }
}
