using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;
using System.Drawing;

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
                        Text = fuelType == FuelType.None ? "<-- Choose Fuel Type -->" : fuelType.ToString(),
                        Disabled = fuelType == FuelType.None
                    })
                    .ToList(),
                Colors = Enum.GetValues(typeof(VechicleColor))
                        .Cast<VechicleColor>()
                        .Select(color => new SelectListItem
                        {
                            Value = color.ToString(),
                            Text = color == VechicleColor.None ? "<-- Choose Color -->" : color.ToString(),
                            Disabled = color == VechicleColor.None
                        })
                        .ToList(),
                Conditions = Enum.GetValues(typeof(Condition))
                        .Cast<Condition>()
                        .Select(condition => new SelectListItem
                        {
                            Value = condition.ToString(),
                            Text = condition == Condition.None ? "<-- Choose Condition -->" : condition.ToString(),
                            Disabled = condition == Condition.None
                        })
                        .ToList()
            };
            return View(model);
        }
        //[HttpPost]
        //public IActionResult Add(VechicleAddViewModel model)
        //{

        //}

    }
}
