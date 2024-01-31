using FINBOURNE.Cache;
using System.Linq.Expressions;

namespace FINBOURNE.GenericCache
{
    public interface ILRUCache<TKey, TItem>
        where TKey : notnull
        where TItem : notnull
    {
        TItem Get(TKey key);
        IEnumerable<KeyValuePair<TKey, TItem>> GetList(IEnumerable<TKey> keys);
        bool TryGetItem(TKey key, out TItem? value);
        void SetItem(TKey key, TItem value);
        void SetItems(Expression<Func<TItem, TKey>> keySelector, IEnumerable<TItem> values);
    }

    public interface ILRUCache<TItem> : ILRUCache<string, TItem>
        where TItem : notnull
    { }

}