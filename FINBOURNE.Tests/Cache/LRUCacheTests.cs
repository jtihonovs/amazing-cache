
using FINBOURNE.LRUCache;
using FINBOURNE.Tests.Cache.Models;
using FluentAssertions;
using Xunit;

namespace FINBOURNE.Tests.Cache
{
    public class LRUCacheTests
    {
        private LRUCache<string, Person> _personCache;

        public LRUCacheTests()
        {
            // Add AllPeopleList to cache
            // FirstName property will remain as a key
            _personCache = new LRUCache<string, Person>(capacity: 3);
            _personCache.SetItems((keyValue) => keyValue.firstName, AllPeopleList); 
        }

        [Fact]
        public void LRUCache_Get_Throw_Not_Found()
        {
            Action act = () => _personCache.Get("RandomKey");
            act.Should().Throw<KeyNotFoundException>()
                .WithMessage("Key: 'RandomKey' was not found!");
        }

        [Theory, MemberData(nameof(Data_Person))]
        public void LRUCache_Get_Found_By_Key(Person person)
        {
            var response = _personCache.Get(person.firstName);
            response.Should().Be(person);
        }

        [Fact]
        public void LRUCache_SetItem_Adds_Item()
        {
            var newPerson = new Person("Bill", "Black");
            _personCache.SetItem(newPerson.firstName, newPerson);
            var response = _personCache.Get(newPerson.firstName);
            response.Should().Be(newPerson);
        }

        public static IEnumerable<object[]> Data_Person{ 
            get {
            
                foreach(var item in AllPeopleList)
                {
                    yield return new object[] { item };
                };
            }
        }
        private static IEnumerable<Person> AllPeopleList
        {
            get
            {
                yield return new Person("Jake", "Gray");
                yield return new Person("Joe", "White");
                yield return new Person("Bob", "Green");
            }
        }
    }
}
