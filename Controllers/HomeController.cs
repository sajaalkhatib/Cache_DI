using Cache_DI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cache_DI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        public IActionResult ClearCache()
        {
            _userService.ClearCache();
            TempData["Message"] = "✅ الكاش تم مسحه بنجاح";
            return RedirectToAction("Index");
        }

        public IActionResult CacheStats()
        {
            var stats = _userService.GetCacheStats();
            return View(stats);
        }

        public IActionResult ViewCache()
        {
            var cacheContents = _userService.GetAllCachedItems();
            ViewBag.CacheKeys = _userService.GetCacheKeys();
            return View(cacheContents);
        }

        // إضافة action جديد لاختبار الكاش
        public IActionResult TestCache()
        {
            Console.WriteLine("🧪 بدء اختبار الكاش...");

            // جلب البيانات عدة مرات لمشاهدة الفرق
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\n🔄 المحاولة #{i}:");
                var users = _userService.GetAllUsers();
            }

            TempData["Message"] = "🧪 تم اختبار الكاش - شاهد الـ Console";
            return RedirectToAction("Index");
        }
    }
}