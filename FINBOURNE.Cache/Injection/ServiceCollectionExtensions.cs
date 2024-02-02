using Microsoft.Extensions.DependencyInjection;

namespace FINBOURNE.LRUCache.Injection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<(Type, Type)> cacheKeyValues)
            => RegisterCacheItemServices(services, cacheKeyValues);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<(Type, Type)> cacheKeyValues, int capacity)
            => RegisterCacheItemServices(services, cacheKeyValues, capacity);

        public static IServiceCollection AddCacheKeyValuePair(this IServiceCollection services, IEnumerable<(Type, Type)> cacheKeycacheValueTypes, int capacity, ServiceLifetime serviceLifetime)
            => RegisterCacheItemServices(services, cacheKeycacheValueTypes, capacity, serviceLifetime);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<(Type, Type)> cacheValueTypes, ServiceLifetime serviceLifetime)
            => RegisterCacheItemServices(services, cacheValueTypes, serviceLifetime: serviceLifetime);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<Type> cacheValueTypes)
            => RegisterCacheItemServices(services, cacheValueTypes);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<Type> cacheValueTypes, int capacity)
            => RegisterCacheItemServices(services, cacheValueTypes, capacity);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<Type> cacheValueTypes, int capacity, ServiceLifetime serviceLifetime)
            => RegisterCacheItemServices(services, cacheValueTypes, capacity, serviceLifetime);

        public static IServiceCollection AddCacheItemTypes(this IServiceCollection services, IEnumerable<Type> cacheValueTypes, ServiceLifetime serviceLifetime)
            => RegisterCacheItemServices(services, cacheValueTypes, serviceLifetime: serviceLifetime);

        // Register Key/Value Cache
        // Key: String; Value: Generic
        private static IServiceCollection RegisterCacheItemServices(IServiceCollection services,
                IEnumerable<Type> cacheValueTypes,
                int capacity = 100,
                ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddTransient(typeof(ILRUCache<>), typeof(LRUCache<>));

            if (cacheValueTypes != null)
            {
                foreach(var type in cacheValueTypes)
                {
                    var genericInterfaceType = GetGenericType(typeof(ILRUCache<>), type);
                    var genericClassType = GetGenericType(typeof(LRUCache<>), type);
                    AddService(services, capacity, serviceLifetime, genericInterfaceType, genericClassType);
                }
            }
            return services;
        }

        // Register Key/Value Cache
        // Key: Generic; Value: Generic
        private static IServiceCollection RegisterCacheItemServices(IServiceCollection services,
                IEnumerable<(Type, Type)> cacheKeyValues,
                int capacity = 100,
                ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddTransient(typeof(ILRUCache<,>), typeof(LRUCache<,>));

            if (cacheKeyValues != null)
            {
                foreach (var (type1, type2) in cacheKeyValues)
                {
                    var genericInterfaceType = GetGenericType(typeof(ILRUCache<,>), type1, type2);
                    var genericClassType = GetGenericType(typeof(LRUCache<,>), type1, type2);
                    AddService(services, capacity, serviceLifetime, genericInterfaceType, genericClassType);
                }
            }
            return services;
        }

        private static void AddService(IServiceCollection services, int capacity, ServiceLifetime serviceLifetime, Type genericInterfaceType, Type genericClassType)
        {
            var classInstance = Activator.CreateInstance(genericClassType, capacity);
            services.Add(new ServiceDescriptor(genericInterfaceType, classInstance, serviceLifetime));
        }

        private static Type GetGenericType(Type type, params Type[] genericTypes)
        {
            return type.MakeGenericType(genericTypes);
        }
    }
}
