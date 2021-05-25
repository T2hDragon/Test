using System;

namespace WebApp.Helpers
{
    /// <summary>
    /// Single demo
    /// </summary>
    public class SingletonDemo :IDiSingleton
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}