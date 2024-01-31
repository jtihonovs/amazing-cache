using FINBOURNE.GenericCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINBOURNE.App.Services
{
    public class Application
    {
        private ILRUCache<string, int> _cache;

        public Application(ILRUCache<string, int> cache)
        {
            _cache = cache;
        }
        public void Run()
        {
            _cache.SetItem("test", 2);
            var res = _cache.Get("test");
            Console.WriteLine($"Res: {res}");
        }
    }
}
