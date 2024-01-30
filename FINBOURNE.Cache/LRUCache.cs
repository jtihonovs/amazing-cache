using FINBOURNE.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FINBOURNE.GenericCache
{
    internal class LRUCache<TKey, TItem> : ILRUCache<TKey, TItem>
        where TKey : notnull
        where TItem : notnull
    {
        [Range(0, int.MaxValue, ErrorMessage = "Capacity for LRUCache should be more than 0.")]
        private readonly int _capacity; 
        private readonly object _balanceLock;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>> _cacheDict;
        private readonly LinkedList<CacheItem<TKey, TItem>> _lruList;

        public LRUCache()
        {
            _capacity = 100; // TODO: default should come from settings 
            _balanceLock = new object();
            _cacheDict = new Dictionary<TKey, LinkedListNode<CacheItem<TKey, TItem>>>();
            _lruList = new LinkedList<CacheItem<TKey, TItem>>(); // Should I use KeyValuePair?
        }

        public LRUCache(int capacity) : this()
        {
            if (capacity > 0) throw new ArgumentException("Capacity for LRUCache should be more than 0.");
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
                throw new Exception("Item was not found"); // TODO: Consider a more accurate exception
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
            lock (_balanceLock)
            {
                if(_cacheDict.TryGetValue(key, out var existingNode))
                {
                    _lruList.Remove(existingNode);
                    _cacheDict.Remove(key);
                } else if(_cacheDict.Count >= _capacity)
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

        private void RemoveFirst()
        {
            LinkedListNode<CacheItem<TKey, TItem>>? node = _lruList.First;
            if (node is not null)
            {
                _lruList.RemoveFirst();
                _cacheDict.Remove(node.Value.Key);
            }
        }

        private void MoveNodeToEnd(LinkedListNode<CacheItem<TKey, TItem>> node)
        {
            _lruList.Remove(node);
            _lruList.AddLast(node);
        }
    }

    internal class LRUCache<TItem> : LRUCache<string, TItem>, ILRUCache<TItem>
        where TItem : notnull
    { }
}
