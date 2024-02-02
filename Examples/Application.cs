using FINBOURNE.LRUCache;

namespace FINBOURNE.App
{
    internal class Application
    {
        private ILRUCache<string, Person> _cache;

        public Application(ILRUCache<string, Person> personCache)
        {
            _cache = personCache;
        }
        public void Run()
        {
            // Set Cached items:
            _cache.SetItem("Jack", new Person("Jack", "Will"));
            _cache.SetItems((key) => key.firstName, [new Person("Bob", "Smith"), new Person("Joe", "Blobs")]);

            // Get items:
            var res = _cache.GetList(["Joe", "Bob", "Jack"]);
            foreach (var item in res)
            {
                Console.WriteLine($"Person item from list: {item}");
            }
            Console.WriteLine($"I used Get to get Bob: {_cache.Get("Bob")}");

            if (_cache.TryGetItem("Jack", value: out Person? hopefullyItsJack))
            {
                Console.WriteLine($"I used TryGet to get Jack: {hopefullyItsJack}");
            }

            // Remove Jack:
            _cache.Remove("Jack");
            if (!_cache.TryGetItem("Jack", value: out Person? jackShouldNotBeHere))
            {
                Console.WriteLine($"I Should not see this about Jack: {jackShouldNotBeHere}");
            }
            else
            {
                Console.WriteLine($"Jack was not found because he was removed");
            }

            // Clear Cache:
            _cache.Clear();
            if (!_cache.TryGetItem("Bob", value: out Person? bob))
            {
                Console.WriteLine($"Bob was not found as the cache was cleared");
            }
        }

    }
    internal record Person(string firstName, string lastName);
}
