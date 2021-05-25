using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApp.Helpers
{
    public class ScopedDemo : IDiScoped
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}