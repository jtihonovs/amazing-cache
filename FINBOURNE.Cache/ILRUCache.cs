using System.Linq.Expressions;

namespace FINBOURNE.LRUCache
{
    public interface ILRUCache : IDisposable
    {
    }

    public interface ILRUCache<TKey, TItem> : ILRUCache
        where TKey : notnull
    {
        TItem Get(TKey key);
        IEnumerable<KeyValuePair<TKey, TItem>> GetList(IEnumerable<TKey> keys);
        bool TryGetItem(TKey key, out TItem? value);
        void SetItem(TKey key, TItem value);
        void SetItems(Expression<Func<TItem, TKey>> keySelector, IEnumerable<TItem> values);
        void Remove(TKey key);
        void Clear();
    }

    public interface ILRUCache<TItem> : ILRUCache<string, TItem>
    { }

}