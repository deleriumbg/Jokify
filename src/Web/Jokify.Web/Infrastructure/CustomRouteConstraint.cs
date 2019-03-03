using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Jokify.Web.Infrastructure
{
    public class CustomRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var value = values.LastOrDefault();
            if (string.IsNullOrWhiteSpace(value.Value?.ToString()))
            {
                return false;
            }

            var parts = value.Value.ToString().Split("-");
            if (parts.Length != 2)
            {
                return false;
            }

            if (parts[1] != "code")
            {
                return false;
            }

            return int.TryParse(parts[0], out _);
        }
    }
}
