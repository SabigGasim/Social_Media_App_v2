
using NativeApp.Constants;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
public class MutedWordsViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private readonly Func<RangeObservableCollection<MutedWordModel>?> _wordsGetter;
    private readonly Action<RangeObservableCollection<MutedWordModel>?> _wordsSetter;
    private readonly Subscriber _subscriber;
    private readonly PropertyChangedEventHandler OnMutedWordsChangedEventHandler;
    private ICommand? _goToWordAdditionPageCommand;

    public MutedWordsViewModel(
        MutedAndBlockedViewModel viewModel,
        INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;
        _wordsGetter = () => viewModel.Model?.MutedWords;
        _wordsSetter = (value) => viewModel.Model!.MutedWords = value;

        _subscriber = viewModel.Subscriber;
        OnMutedWordsChangedEventHandler = (sender, e) =>
        {
            if(e.PropertyName == nameof(MutedAndBlockedViewModel.Model))
            {
                OnPropertyChanged(nameof(Words));
            }
        };

        SubscribeToMutedWords();
    }

    public RangeObservableCollection<MutedWordModel>? Words
    {
        get => _wordsGetter();
        set
        {
            _wordsSetter(value);
            OnPropertyChanged(nameof(Words));
        }
    }

    public ICommand? GoToWordAdditionPageCommand =>
        _goToWordAdditionPageCommand ??= _navigateCommandFactory.Create(Routes.AddMutedWordPage);

    private void SubscribeToMutedWords() => _subscriber.Subscribe(OnMutedWordsChangedEventHandler);
}
