using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Query;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.Controllers
{
    public class QueryController : Controller
    {
        private readonly NVMotorsDbContext context;
        public QueryController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = context.Queries.Where(q => q.RequesterId == GetCurrentUserId())
                .Select(q => new QueryIndexViweModel
                {
                    AdId = q.AdId,
                    Description = q.Description,
                    Make = q.Ad.Motor.Make,
                    Model = q.Ad.Motor.Model,
                    Date = q.DateRequested.ToString("dd/MM/yyyy"),
                }).ToList();
            return View(query);
        }
        public Guid GetCurrentUserId()
        {
            if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
        [HttpPost]
        public async Task<IActionResult> MakeQuery(MakeQueryViewModel queryModel)
        {
            if (!ModelState.IsValid) 
            {
                //ViewData[nameof(Error)] = "Error ";
                return RedirectToAction("Details", "Ad", new {id = queryModel.AdId});
            }

            var query = new Query()
            {
                PhoneNumber = queryModel.PhoneNumber,
                Description = queryModel.Description,
                AdId = queryModel.AdId,
                RequesterId = GetCurrentUserId(),
                DateRequested = DateTime.Now,
            }; 
            await context.Queries.AddAsync(query);
            await context.SaveChangesAsync();
            //TempData(nameof(Succsec))
            return RedirectToAction("Details", "Ad", new { id = queryModel.AdId });

        }
    }
}
