using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaShop.Controllers
{
    public class RoleAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public RoleAuthorizationFilter(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == _requiredRole))
            {
                context.Result = new ForbidResult(); // Redirect or throw an exception
            }
        }
    }
}
