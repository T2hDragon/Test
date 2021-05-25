using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApp.Helpers
{
    /// <summary>
    /// Scoped Demo
    /// </summary>
    public class ScopedDemo : IDiScoped
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}