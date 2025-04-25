using NativeApp.Infrastructure.Data.Persistence;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.Validations.Rules;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class AddPostViewModel : ViewModelBase
{
    private readonly Func<PostViewModel, Task> _addMethod;
    private readonly Func<PostViewModel> _injectPostViewModel;
    private readonly BindableAccountsAccessor _accountsAccessor;
    private readonly Interfaces.IMediaPicker _mediaPicker;
    private readonly INavigationService _navigationService;
    private ICommand? _addPostCommand;
    private ICommand? _pickPhotosCommand;
    private bool _isValid;
    private ICommand? _validateCommand;

    public AddPostViewModel(
        TimelineViewModel timeline,
        BindableAccountsAccessor accountsAccessor,
        Interfaces.IMediaPicker mediaPicker,
        INavigationService navigationService,
        IServiceProvider serviceProvider)
    {
        _addMethod = timeline.AddPost;
        _injectPostViewModel = serviceProvider.GetRequiredService<PostViewModel>;
        _accountsAccessor = accountsAccessor;
        _mediaPicker = mediaPicker;
        _navigationService = navigationService;

        Media.Value = new();

        AddValidations();
        Validate();
    }

    private void AddValidations()
    {
        Text.Validations!.AddRange(new IValidationRule<string>[]
        {
            new IsNotNullOrEmptyRule<string>(),
            new IsNotWhiteSpaceRule<string>(),
        });
        Media.Validations!.Add(new IsInLengthRule<MediaListModel>(1, 5));
    }

    public ValidatableObject<string> Text { get; private set; } = new();

    public ValidatableObject<MediaListModel> Media { get; private set; } = new();

    public bool IsValid
    {
        get => _isValid;
        private set => TrySetValue(ref _isValid, value);
    }

    public ICommand? PickPhotosCommand => _pickPhotosCommand ??= new Command(async () =>
    {
        var photos = (await _mediaPicker.PickPhotosAsync()).ToArray();

        Media!.Value!.AddRange(photos);

        Validate();
    });

    public ICommand? AddPostCommand => _addPostCommand ??= new Command(async () => await AddPostAsync());
    public ICommand? ValidateCommand => _validateCommand ??= new Command(Validate);

    private async Task AddPostAsync()
    {
        var post = new PostModel
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Text = Text.Value!,
            Media = this.Media!.Value?.DeepCopy(),
            User = _accountsAccessor.CurrentAccount!.User!,
            IsLiked = false,
            CommentsCount = 0,
            Likes = 0
        };

        var viewModel = _injectPostViewModel();
        viewModel.Post = post;

        await _addMethod(viewModel);

        await _navigationService.PopAsync();

        Clear();
        Validate();
    }

    private void Validate()
    {
        IsValid = Text.Validate() || Media.Validate();
    }

    private void Clear()
    {
        Media.Value?.Clear();
        Text.Value = "";
    }
}
