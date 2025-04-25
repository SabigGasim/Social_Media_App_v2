using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class UserSearchViewModel : ViewModelBase
{
    private readonly ILookupHandler _handler;
    private readonly IUserLookupRepository _repository;
    private RangeObservableCollection<UserModel>? _user = new();
    private ICommand? _performSearchCommand;

    public UserSearchViewModel(
        ILookupHandler handler,
        IUserLookupRepository repository)
    {
        _handler = handler;
        _repository = repository;

        _handler.Interval = 700d;
        _handler.Handle = SearchForUsers;
        _performSearchCommand = new Command((object text) => _handler.Reset((string)text));
    }

    public RangeObservableCollection<UserModel>? Users 
    {
        get => _user; 
        set => TrySetValue(ref _user, value); 
    }

    public ICommand? PerformSearchCommand => _performSearchCommand;

    //repeating User.Clear multiple times helps reduce the small delay
    //that occurs when the list is cleared, but the data hasn't been retrieved yet
    private async Task SearchForUsers(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            Users!.Clear();
            return;
        }

        var result = await _repository.FindManyByUsername(query);
        if (result.Success && result.Value!.Any())
        {
            var values = result.Value!.Map().ToList();
            Users!.Clear();
            Users!.AddRange(values);
            return;
        }

        Users!.Clear();
    }
}
