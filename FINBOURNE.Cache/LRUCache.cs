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
                // Code
            }
        }

        public TItem GetList<T>(IEnumerable<string> keys)
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
