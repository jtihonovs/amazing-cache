using FINBOURNE.LRUCache.Model;
using System.Linq.Expressions;

namespace FINBOURNE.LRUCache
{
    // Least Recently Used (LRU) Cache - organizes items in order of use
    public class LRUCache<TKey, TItem> : ILRUCache<TKey, TItem>
        where TKey : notnull
    {
        private readonly int _capacity; // Max number of cached items
        private readonly object _balanceLock;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>> _cacheDict;
        private readonly LinkedList<CacheItem<TKey, TItem>> _lruList;

        public LRUCache()
        {
            _capacity = 100;
            _balanceLock = new object();
            _cacheDict = new Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>>();
            _lruList = new LinkedList<CacheItem<TKey, TItem>>();
        }

        public LRUCache(int capacity) : this()
        {
            if (capacity < 1) throw new ArgumentException("Capacity for LRUCache should be at least 1.");
            _capacity = capacity;
        }

        public TItem Get(TKey key)
        {
            lock (_balanceLock)
            {
                LinkedListNode<CacheItem<TKey, TItem>>? node;
                if (_cacheDict.TryGetValue(key, out node))
                {
                    MoveNodeToEnd(node);
                    return node.Value.Value;
                }
                throw new KeyNotFoundException($"Key: '{key}' was not found!"); 
            }
        }

        public IEnumerable<KeyValuePair<TKey, TItem>> GetList(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                yield return new KeyValuePair<TKey, TItem>(key, Get(key));
            }
        }

        public bool TryGetItem(TKey key, out TItem? value)
        {
            lock (_balanceLock)
            {
                LinkedListNode<CacheItem<TKey, TItem>>? node;
                if (_cacheDict.TryGetValue(key, out node))
                {
                    MoveNodeToEnd(node);
                    value = node.Value.Value;
                    return true;
                }
                value = default;
                return false;
            }
        }

        public void SetItem(TKey key, TItem value)
        {

            if(key is null)
            {
                throw new ArgumentNullException("Key cannot be null");
            }

            lock (_balanceLock)
            {
                RemoveIfExists(key);
                if(_cacheDict.Count >= _capacity)
                {
                    RemoveFirst();
                }

                var item = new CacheItem<TKey, TItem>(key, value);
                var node = new LinkedListNode<CacheItem<TKey, TItem>>(item);
                _lruList.AddLast(node);
                _cacheDict.Add(key, node);
            }
        }

        public void SetItems(Expression<Func<TItem, TKey>> keyFunc, IEnumerable<TItem> values)
        {
            Func<TItem, TKey> getKeyCompiler = keyFunc.Compile();
            foreach (var value in values)
            {
                var key = getKeyCompiler(value);
                if(key is not null)
                {
                    SetItem(key, value);
                }
            }
        }

        public void Clear()
        {
            lock (_balanceLock)
            {
                _lruList.Clear();
                _cacheDict.Clear();
            }
        }

        public void Remove(TKey key)
        {
            lock(_balanceLock)
            {
                var isRemoved = RemoveIfExists(key);
                if (!isRemoved)
                {
                    throw new KeyNotFoundException($"Could not remove cache item by the key: {key}");
                }
            }
        }

        private void RemoveFirst()
        {
            LinkedListNode<CacheItem<TKey, TItem>>? node = _lruList.First;
            if (node is not null)
            {
                _lruList.RemoveFirst();
                _cacheDict.Remove(node.Value.Key);
            }
        }

        private bool RemoveIfExists(TKey key)
        {
            if (_cacheDict.TryGetValue(key, out var existingNode))
            {
                _lruList.Remove(existingNode);
                _cacheDict.Remove(key);
                return true;
            }
            return false;
        }

        private void MoveNodeToEnd(LinkedListNode<CacheItem<TKey, TItem>> node)
        {
            _lruList.Remove(node);
            _lruList.AddLast(node);
        }

        public void Dispose()
        {
            Clear();
        }
    }

    public class LRUCache<TItem> : LRUCache<string, TItem>, ILRUCache<TItem>
    {
        public LRUCache() : base() { }
        public LRUCache(int capacity) : base(capacity) { }
    }
}
