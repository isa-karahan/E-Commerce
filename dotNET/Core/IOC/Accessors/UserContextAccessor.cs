using Microsoft.AspNetCore.Http;

namespace Core.IOC.Accessors
{
    public class UserContextAccessor : IUserContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UserId => Convert.ToInt64(_httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value);
    }
}
