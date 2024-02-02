using FINBOURNE.GenericCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINBOURNE.Cache.Model
{
    internal class CacheItem<TKey, TItem>
    {
        public TKey Key { get; init; }
        public TItem Value { get; init; }
        public CacheItem(TKey keyIn, TItem valueIn) => (Key, Value) = (keyIn, valueIn);
    }
}
