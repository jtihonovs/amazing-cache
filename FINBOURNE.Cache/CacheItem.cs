using FINBOURNE.GenericCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINBOURNE.Cache
{
    internal record CacheItem<TKey, TItem>(TKey Key, TItem Value) : ICacheItem<TKey, TItem>;

    public interface ICacheItem<TKey, TItem>
    {
        public TKey Key { get; }
        public TItem Value { get; }

    }
}
