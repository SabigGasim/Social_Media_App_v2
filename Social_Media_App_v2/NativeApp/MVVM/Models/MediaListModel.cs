using NativeApp.Helpers;

namespace NativeApp.MVVM.Models;

public class MediaListModel : RangeObservableCollection<MediaModel>
{
    public MediaListModel()
    {
    }

    public MediaListModel(IEnumerable<MediaModel> collection) : base(collection)
    {
    }

    public MediaListModel(List<MediaModel> list) : base(list)
    {
    }

    public MediaListModel This => this;
}
