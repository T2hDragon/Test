using System;

namespace WebApp.Helpers
{
    public class TransientDemo : IDiTransient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}