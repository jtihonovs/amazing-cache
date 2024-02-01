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

            _cache.SetItem("test 2", 2);
            var res2 = _cache.Get("test 2");
            Console.WriteLine($"Res: {res}");

            res = _cache.Get("test");
            Console.WriteLine($"Res: {res}");


        }
    }
}
