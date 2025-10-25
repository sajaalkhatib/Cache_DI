CacheDemo is an ASP.NET Core MVC project that demonstrates how to efficiently use Memory Cache (RAM) for temporary data storage while maintaining a permanent copy in a JSON file.
The main goal is to show how caching can improve performance, track cache hits and misses, and persist data beyond application restarts.

How it works:

Data is first retrieved from Memory Cache (RAM) for fast access.

If data is missing (CACHE MISS), default data is created: Saja, Ali, Saleh, Lina, Omar.

The data is then stored in RAM and in a JSON file (usersCache.json) as a permanent backup.

The system tracks the number of cache hits and misses using console logs and logging.


CacheDemo/
│
├── Controllers/
│   └── HomeController.cs        # Handles HTTP requests and responses
├── Services/
│   ├── IUserService.cs          # Service interface for user operations
│   └── UserService.cs           # Implements caching and file storage logic
├── Views/
│   └── Home/
│       └── Index.cshtml         # Main view displaying user data
├── Program.cs                   # Application startup and DI configuration
└── usersCache.json              # JSON file storing permanent cache (auto-created)
