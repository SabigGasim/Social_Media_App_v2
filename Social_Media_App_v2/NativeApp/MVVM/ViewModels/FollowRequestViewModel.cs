using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels;

public class FollowRequestViewModel : ViewModelBase
{
    private RangeObservableCollection<FollowRequestModel>? _requests = new();
    private readonly IFollowRequestsRepository _repository;

    public FollowRequestViewModel(IFollowRequestsRepository repository)
    {
        _repository = repository;
    }

    public async Task UpdateFollowReuqests(Guid? lastSeenReuqest, int numberOfReuqests)
    {
        var result = (await _repository.GetFollowRequests(null, lastSeenReuqest, numberOfReuqests));
        if (result.Success)
        {
            Requests?.AddRange(result.Value!.Map());
        }
    }

    public RangeObservableCollection<FollowRequestModel>? Requests 
    {
        get => _requests;
        set => TrySetValue(ref _requests, value);
    }
}
