using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINBOURNE.Cache
{
    internal record CacheItem<TKey, TItem>(TKey key, TItem Value);
}
