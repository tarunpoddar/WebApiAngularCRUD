using Microsoft.AspNetCore.Http;

namespace Crud.Shared
{
    /// <summary>
    /// Extention methods for <see cref="HttpRequest"/>.
    /// </summary>
    public static class HttpRequestHelper
    {
        const string PasswordKeyHeader = "passwordKey";
        const string ValidPasswordKeyValue = "passwordKey123456789";

        /// <summary>
        /// Checks if the request is valid which contains the required password header and value.
        /// </summary>
        /// <param name="theHttpRequest">The Http request.</param>
        /// <returns>True, if request is valid else false.</returns>
        public static bool isValidRequest(this HttpRequest theHttpRequest)
        {
            if (theHttpRequest.Headers.TryGetValue(PasswordKeyHeader, out var passwordKeyValues))
            {
                return passwordKeyValues.Any(value => value == ValidPasswordKeyValue);
            }

            return false;
        }
    }
}