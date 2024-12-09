using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NVMotors.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Guid GetCurrentUserId()
        {
            if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
    }
}
