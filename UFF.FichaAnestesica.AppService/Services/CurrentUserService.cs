using Microsoft.AspNetCore.Http;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public int? UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;
                return int.TryParse(value, out var id) ? id : null;
            }
        }

        public bool IsAdmin =>
            _httpContextAccessor.HttpContext?.User?.FindFirst("is_admin")?.Value == "true";
    }
}
