using System;

namespace WebApp.Helpers
{
    public class SingletonDemo :IDiSingleton
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}