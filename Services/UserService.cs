using Microsoft.Extensions.Caching.Memory;

namespace Cache_DI.Services
{
    public class UserService : IUserService
    {
        private readonly IMemoryCache _cache;
        private static int _cacheHits = 0;
        private static int _cacheMisses = 0;
        private readonly ILogger<UserService> _logger;

        public UserService(IMemoryCache cache, ILogger<UserService> logger)
        {
            _cache = cache;
            _logger = logger;
            Console.WriteLine("🎯 UserService initialized - الكاش جاهز");
            _logger.LogInformation("🎯 UserService initialized - الكاش جاهز");
        }

        public List<string> GetAllUsers()
        {
            Console.WriteLine($"\n🔍 [{DateTime.Now:HH:mm:ss}] بدء جلب البيانات...");
            _logger.LogInformation($"🔍 [{DateTime.Now:HH:mm:ss}] بدء جلب البيانات...");

            bool hasCachedData = _cache.TryGetValue("usersCache", out List<string> users);

            Console.WriteLine($"📊 قبل العملية: الكاش {(hasCachedData ? "🟢 فيه بيانات" : "🔴 فارغ")}");
            _logger.LogInformation($"📊 قبل العملية: الكاش {(hasCachedData ? "🟢 فيه بيانات" : "🔴 فارغ")}");

            if (!hasCachedData)
            {
                _cacheMisses++;
                users = new List<string> { "Saja", "Ali", "Saleh", "Lina", "Omar" };

                Console.WriteLine($"🆕 تم إنشاء بيانات جديدة: {string.Join(", ", users)}");
                _logger.LogInformation($"🆕 تم إنشاء بيانات جديدة: {string.Join(", ", users)}");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _cache.Set("usersCache", users, cacheOptions);

                Console.WriteLine("💾 تم حفظ البيانات في الكاش (RAM)");
                _logger.LogInformation("💾 تم حفظ البيانات في الكاش (RAM)");

                PrintCacheMiss();
            }
            else
            {
                _cacheHits++;
                Console.WriteLine($"✅ تم العثور على البيانات في الكاش: {string.Join(", ", users)}");
                _logger.LogInformation($"✅ تم العثور على البيانات في الكاش: {string.Join(", ", users)}");

                PrintCacheHit();
            }

            return users;
        }

        private void PrintCacheMiss()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║               🟢 CACHE MISS             ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine($"║ 📦 البيانات خزنت في الذاكرة (RAM)     ║");
            Console.WriteLine($"║ 🔢 رقم التخزين: {_cacheMisses,-3}                 ║");
            Console.WriteLine("║ 🗺️  الموقع: ذاكرة التطبيق العشوائية   ║");
            Console.WriteLine("║ ⏱️  تنتهي بعد: دقيقتين                 ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            _logger.LogInformation($"🟢 CACHE MISS - التخزين رقم: {_cacheMisses}");
        }

        private void PrintCacheHit()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║               🔵 CACHE HIT              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ 📥 البيانات جلبت من الذاكرة (RAM)     ║");
            Console.WriteLine($"║ 🔢 رقم الجلب: {_cacheHits,-4}                   ║");
            Console.WriteLine("║ 🗺️  الموقع: ذاكرة التطبيق العشوائية   ║");
            Console.WriteLine("║ ⚡ أسرع من قاعدة البيانات              ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            _logger.LogInformation($"🔵 CACHE HIT - الجلب رقم: {_cacheHits}");
        }

        public void ClearCache()
        {
            _cache.Remove("usersCache");
            Console.WriteLine("\n🗑️ تم مسح الكاش من الذاكرة");
            _logger.LogInformation("🗑️ تم مسح الكاش من الذاكرة");
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
    }
}