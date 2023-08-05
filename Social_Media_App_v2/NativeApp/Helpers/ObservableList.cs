using System.Collections.Specialized;

namespace NativeApp.Helpers;
public class ObservableList<T> : List<T>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public ObservableList(){}

    public ObservableList(IEnumerable<T> collection) : base(collection){}

    public ObservableList(int capacity) : base(capacity){}


    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }

    public new void Add(T item)
    {
        base.Add(item);
        OnCollectionChanged(CollectionChangedEventArgs.Add(item));
    }

    public new void AddRange(IEnumerable<T> collection)
    {
        base.AddRange(collection);
        OnCollectionChanged(CollectionChangedEventArgs.Add(collection.ToList()));
    }

    public new void Clear()
    {
        base.Clear();
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        OnCollectionChanged(CollectionChangedEventArgs.Add(item, index));
    }

    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        base.InsertRange(index, collection);
        OnCollectionChanged(CollectionChangedEventArgs.Add(collection.ToList(), index));
    }

    public new bool Remove(T item)
    {
        var index = base.IndexOf(item);
        if (index != -1)
        {
            base.Remove(item);
            OnCollectionChanged(CollectionChangedEventArgs.Remove(item, index));
            return true;
        }
        return false;
    }

    public new void RemoveAt(int index)
    {
        var item = base[index];
        base.RemoveAt(index);
        OnCollectionChanged(CollectionChangedEventArgs.Remove(item, index));
    }

    public new void RemoveRange(int index, int count)
    {
        var items = base.GetRange(index, count);
        base.RemoveRange(index, count);
        OnCollectionChanged(CollectionChangedEventArgs.Remove(items, index));
    }

    public new void Reverse()
    {
        base.Reverse();
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Reverse(int index, int count)
    {
        base.Reverse(index, count);
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Sort()
    {
        base.Sort();
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Sort(Comparison<T> comparison)
    {
        base.Sort(comparison);
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Sort(IComparer<T> comparer)
    {
        base.Sort(comparer);
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    public new void Sort(int index, int count, IComparer<T> comparer)
    {
        base.Sort(index, count, comparer);
        OnCollectionChanged(CollectionChangedEventArgs.Reset());
    }

    private static class CollectionChangedEventArgs
    {
        public static NotifyCollectionChangedEventArgs Reset() => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        public static NotifyCollectionChangedEventArgs Add(T item) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);
        public static NotifyCollectionChangedEventArgs Add(List<T> collection) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection);
        public static NotifyCollectionChangedEventArgs Add(List<T> collection, int index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, index);
        public static NotifyCollectionChangedEventArgs Add(T item, int index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index);
        public static NotifyCollectionChangedEventArgs Remove<T>(List<T> items, int index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, index);
        public static NotifyCollectionChangedEventArgs Remove<T>(T item, int index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index);
    }
}