namespace FINBOURNE.LRUCache.Model
{
    internal class CacheItem<TKey, TItem>
    {
        public TKey Key { get; init; }
        public TItem Value { get; init; }
        public CacheItem(TKey keyIn, TItem valueIn) => (Key, Value) = (keyIn, valueIn);
    }
}
