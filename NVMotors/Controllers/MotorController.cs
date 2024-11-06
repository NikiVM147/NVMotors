using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;
using System.Drawing;

namespace NVMotors.Web.Controllers
{
    public class MotorController : Controller
    {
        private readonly NVMotorsDbContext context;
        public MotorController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new MotorAddViewModel();
            model.TransmissionTypes = GetEnumSelectList<TransmissionType>();
            model.FuelTypes = GetEnumSelectList<FuelType>();
            model.MotorColors = GetEnumSelectList<MotorColor>();
            model.Conditions = GetEnumSelectList<Condition>();

            return View(model);
        }
        //[HttpPost]
        //public async Task<IActionResult> Add(VechicleAddViewModel modelAdd)
        //{


        //    context.Specifications.Add(specification);
        //    context.Vechicles.Add(vechicle);
        //    await context.SaveChangesAsync();

        //    return View();
        //}

        private List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {
            
            return Enum.GetValues(typeof(TEnum))
                            .Cast<TEnum>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString() == "None" ? "Choose type" : e.ToString(),
                                Disabled = e.ToString() == "None"
                            })
                            .ToList();
        }


    }
}
