using Bogus;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;

namespace NativeApp.Infrastructure.Data.Databases.InMemory;
public class FakeInMemoryDatabase : IDatabase
{
    private readonly List<PostModel> _posts;
    private readonly List<CommentModel> _comments;
    private readonly List<ReplyModel> _replies;
    private readonly List<UserModel> _users;

    public FakeInMemoryDatabase()
    {
        _comments = new();
        _replies = new();
        _users = new();
        _posts = GetPosts(100).ToList();

        foreach (var post in _posts)
        {
            _users.Add(post.User);
            _comments.AddRange(post.Comments);
            post.CommentsCount = post.Comments.Count;

            foreach (var comment in post.Comments)
            {
                _users.Add(comment.User);
                _replies.AddRange(comment.Replies);

                foreach (var reply in comment.Replies)
                {
                    _users.Add(reply.User);
                }
            }
        }
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
            _posts.AddRange(GetPosts(10));
            return Task.CompletedTask;
        }

        _posts.AddRange(posts);
        return Task.CompletedTask;
    }

    private static IEnumerable<PostModel> GetPosts(int numberOfPosts)
    {
        var mediaFaker = GetMediaFaker();
        var userFaker = GetUserFaker(mediaFaker);
        var replyFaker = GetReplyFaker(userFaker);
        var commentsFaker = GetCommentsFaker(userFaker, replyFaker);
        var postFaker = GetPostsFaker(userFaker, commentsFaker, mediaFaker);

        var posts = postFaker.Generate(numberOfPosts);

        return posts;
    }

    private static Faker<PostModel> GetPostsFaker(Faker<UserModel> userFaker, Faker<CommentModel> commentsFaker, Faker<MediaModel> mediaFaker)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());

        return new Faker<PostModel>()
            .RuleFor(x => x.User, () => userFaker.Generate())
            .RuleFor(x => x.Comments, () => new(commentsFaker.GenerateBetween(10, 100)))
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
            .RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now))
            .RuleFor(x => x.Id, Guid.NewGuid);
    }

    private static Faker<MediaModel> GetMediaFaker()
    {
        return new Faker<MediaModel>()
            .RuleFor(x => x.Url, x => x.Image.LoremFlickrUrl(1920, 1080));
    }

    private static Faker<CommentModel> GetCommentsFaker(Faker<UserModel> fakeUser, Faker<ReplyModel> replyFaker)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        return new Faker<CommentModel>()
            .RuleFor(comment => comment.Text, f => f.Lorem.Text())
            .RuleFor(comment => comment.User, () => fakeUser.Generate())
            .RuleFor(comment => comment.Date, f => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
            .RuleFor(comment => comment.Likes, () => randomizer.Next(1_000_000_000))
            .RuleFor(comment => comment.Replies, () => new(replyFaker.GenerateBetween(1, 10)));
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
}
