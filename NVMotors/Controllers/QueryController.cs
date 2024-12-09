using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;

namespace NVMotors.Web.Controllers
{
    public class QueryController : Controller
    {
        private readonly NVMotorsDbContext context;
        public QueryController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MakeQuery()
        {
            return View();
        }
    }
}
