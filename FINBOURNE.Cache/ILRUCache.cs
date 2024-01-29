using System.Linq.Expressions;

namespace FINBOURNE.GenericCache
{
    public interface ILRUCache<TKey, TItem>
    {
        TItem Get<T>(TKey key);
        TItem GetList<T>(IEnumerable<string> keys);
        bool TryGetItem<T>(TKey key, out TItem value);
        void SetItem(TKey key, TItem value);
        void SetItems(Expression<Func<TItem, TKey>> keySelector, IEnumerable<TItem> values);
    }

    public interface ILRUCache<TItem> : ILRUCache<string, TItem> { }
}