The ASP.NET Core MVC project demonstrates how to use Memory Cache to temporarily store data (RAM) while also keeping a permanent copy in a JSON file.
The project utilizes Dependency Injection (DI) to manage services professionally and includes Console Logging for every CACHE HIT and CACHE MISS.

Project Idea

Data is retrieved from Memory Cache (RAM) if available → faster than querying a database.

If data is not present in the cache (CACHE MISS):

Default data is created: Saja, Ali, Saleh, Lina, Omar.

The data is stored in both cache and a JSON file (usersCache.json) for a permanent copy.

The project includes tools to monitor cache and track hits and misses.

Cache can be cleared, and data will be restored from the file on the next request.

CacheDemo/
│
├── Controllers/
│   └── HomeController.cs
├── Services/
│   ├── IUserService.cs
│   └── UserService.cs
├── Views/
│   └── Home/
│       └── Index.cshtml
├── Program.cs
└── usersCache.json  (created automatically)

