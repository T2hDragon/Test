using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Debugging
{
    /// <summary>
    /// Error messages
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Get model state errors
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            return string.Join(" | ", modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
        }
    }
}