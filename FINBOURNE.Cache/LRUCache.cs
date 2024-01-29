using FINBOURNE.Cache;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FINBOURNE.GenericCache
{
    internal class LRUCache<TKey, TItem> : ILRUCache<TKey, TItem>
        where TKey : notnull
    {
        private readonly object _balanceLock;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>> _cacheDict;
        private readonly LinkedList<CacheItem<TKey, TItem>> _lruList;

        public LRUCache()
        {
            _balanceLock = new object();
            _cacheDict = new Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>>();
            _lruList = new LinkedList<CacheItem<TKey, TItem>>();
        }

        public TItem Get<T>(TKey key)
        {
            lock (_balanceLock)
            {
                LinkedListNode<CacheItem<TKey, TItem>>? node;
                if (_cacheDict.TryGetValue(key, out node))
                {
                    // Remove used node
                    // Add new node
                    return node.Value.Value;
                }
                throw new Exception("Item was not found"); // TODO: Consider a more accurate exception
            }
        }

        public IEnumerable<ICacheItem<TKey, TItem>> GetList<T>(IEnumerable<string> keys)
        {
            lock (_balanceLock)
            {
                // Code
            }
        }

        public bool TryGetItem<T>(TKey key, out TItem value)
        {
            lock (_balanceLock)
            {
                // Code
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void SetItem(TKey key, TItem value)
        {
            lock (_balanceLock)
            {
                // Code
            }
        }

        public void SetItems(Expression<Func<TItem, TKey>> keySelector, IEnumerable<TItem> values)
        {
            lock (_balanceLock)
            {
                // Code
            }
        }
    }

    internal class LRUCache<TItem> : LRUCache<string, TItem>, ILRUCache<TItem>
    { }
}
