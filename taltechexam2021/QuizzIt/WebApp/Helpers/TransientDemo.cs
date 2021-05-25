using System;

namespace WebApp.Helpers
{
    /// <summary>
    /// Transient Demo
    /// </summary>
    public class TransientDemo : IDiTransient
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}