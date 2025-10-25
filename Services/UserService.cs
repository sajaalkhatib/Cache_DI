using Microsoft.Extensions.Caching.Memory;

namespace Cache_DI.Services
{
    public class UserService : IUserService
    {
        private readonly IMemoryCache _cache;
        private static int _cacheHits = 0;
        private static int _cacheMisses = 0;

        public UserService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<string> GetAllUsers()
        {
            PrintCacheStatus(); // عرض حالة الكاش قبل

            if (!_cache.TryGetValue("usersCache", out List<string> users))
            {
                _cacheMisses++;
                users = new List<string> { "Saja", "Ali", "Saleh", "Lina", "Omar" };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _cache.Set("usersCache", users, cacheOptions);

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          🟢 CACHE MISS              ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine($"║ 📦 تم التخزين في الذاكرة (RAM)    ║");
                Console.WriteLine($"║ 🔢 مرات التخزين: {_cacheMisses,-4}          ║");
                Console.WriteLine($"║ 📊 البيانات: {string.Join(", ", users)} ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
            }
            else
            {
                _cacheHits++;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          🔵 CACHE HIT               ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine($"║ 📥 تم الجلب من الذاكرة (RAM)      ║");
                Console.WriteLine($"║ 🔢 مرات الجلب: {_cacheHits,-5}            ║");
                Console.WriteLine($"║ 📊 البيانات: {string.Join(", ", users)} ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
            }

            PrintCacheStatus(); // عرض حالة الكاش بعد
            return users;
        }

        public void ClearCache()
        {
            _cache.Remove("usersCache");
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          🟡 CACHE CLEARED           ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║ 🗑️  تم مسح الكاش من الذاكرة        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        }

        public (int hits, int misses) GetCacheStats()
        {
            return (_cacheHits, _cacheMisses);
        }

        public Dictionary<string, object> GetAllCachedItems()
        {
            var items = new Dictionary<string, object>();
            var users = _cache.Get("usersCache") as List<string>;
            if (users != null)
            {
                items.Add("usersCache", users);
            }
            return items;
        }

        public List<string> GetCacheKeys()
        {
            var keys = new List<string>();
            if (_cache.TryGetValue("usersCache", out _))
            {
                keys.Add("usersCache");
            }
            return keys;
        }

        public object GetCachedItem(string key)
        {
            _cache.TryGetValue(key, out object value);
            return value;
        }

        private void PrintCacheStatus()
        {
            var hasData = _cache.TryGetValue("usersCache", out _);
            Console.WriteLine($"🔍 حالة الكاش: {(hasData ? "🟢 يحتوي على بيانات" : "🔴 فارغ")}");
        }
    }
}