using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NVMotors.Data;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;

namespace NVMotors.Web.Controllers
{
    public class VechicleController : Controller
    {
        private readonly NVMotorsDbContext context;
        public VechicleController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new VechicleAddViewModel
            {
                FuelTypes = Enum.GetValues(typeof(FuelType))
                        .Cast<FuelType>()
                        .Select(fuelType => new SelectListItem
                        {
                            Value = fuelType.ToString(),
                            Text = fuelType.ToString()
                        })
                        .ToList(),
                Colors = Enum.GetValues(typeof(Color))
                        .Cast<Color>()
                        .Select(color => new SelectListItem
                        {
                            Value = color.ToString(),
                            Text = color.ToString()
                        })
                        .ToList(),
                Conditions = Enum.GetValues(typeof(Condition))
                        .Cast<Condition>()
                        .Select(condition => new SelectListItem
                        {
                            Value = condition.ToString(),
                            Text = condition.ToString()
                        })
                        .ToList()
            };
            return View(model);
        }
    }
}
