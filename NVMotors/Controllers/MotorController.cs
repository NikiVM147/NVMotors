using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;
using System.Drawing;
using System.Security.Claims;

namespace NVMotors.Web.Controllers
{
    //TODO categories, services, async, ads, input for cubics
    public class MotorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly NVMotorsDbContext context;
        public MotorController(NVMotorsDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new MotorAddViewModel
            {
                TransmissionTypes = GetEnumSelectList<TransmissionType>(),
                FuelTypes = GetEnumSelectList<FuelType>(),
                MotorColors = GetEnumSelectList<MotorColor>(),
                Conditions = GetEnumSelectList<Condition>()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MotorAddViewModel addModel)
        {
            var check = addModel;
            if (!ModelState.IsValid)
            {
                addModel.TransmissionTypes = GetEnumSelectList<TransmissionType>();
                addModel.FuelTypes = GetEnumSelectList<FuelType>();
                addModel.MotorColors = GetEnumSelectList<MotorColor>();
                addModel.Conditions = GetEnumSelectList<Condition>();

                return View(addModel);
            }

            var specification = new Specification
            {
                Year = addModel.Year,
                HorsePower = addModel.HorsePower,
                TransmissionType = addModel.SelectedTransmissionType.ToString(),
                FuelType = addModel.SelectedFuelType.ToString(),
                Color = addModel.SelectedColor.ToString(),
                Condition = addModel.SelectedCondition.ToString(),
            };

            var motor = new Motor
            {
                Make = addModel.Make,
                Model = addModel.Model,
                Specification = specification,
                SellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            context.Specifications.Add(specification);
            context.Motors.Add(motor);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

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
