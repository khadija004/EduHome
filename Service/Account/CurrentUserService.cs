using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Service.Account
{
    public class CurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            if (context != null)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                UserId = string.IsNullOrEmpty(userId) ? null : userId;
                IsAuthenticated = userId != null;
            }
        }
        public string UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
