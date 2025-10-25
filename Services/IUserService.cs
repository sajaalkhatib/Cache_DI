namespace Cache_DI.Services
{
    public interface IUserService
    {
        List<string> GetAllUsers();
        void ClearCache();
        (int hits, int misses) GetCacheStats();
        Dictionary<string, object> GetAllCachedItems();
        List<string> GetCacheKeys();
        object GetCachedItem(string key);
    }
}