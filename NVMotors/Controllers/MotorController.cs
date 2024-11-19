using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;
using System.Drawing;
using System.Security.Claims;

namespace NVMotors.Web.Controllers
{
    //TODO categories, services, async, ads, input for cubics, remove UM
    [Authorize]
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
            var model = context.Motors.Where(m => m.Seller.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
                .Select(m => new MotorIndexViewModel
                {
                    Id = m.Id,
                    Make = m.Make,
                    Model = m.Model,
                    Year = m.Specification.Year,
                }).ToList();
            return View(model);
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
                EngineDisplacement = addModel.EngineDisplacement,
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
                SellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            context.Specifications.Add(specification);
            context.Motors.Add(motor);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var motor = context.Motors.Include(x => x.Specification).FirstOrDefault(x => x.Id == id);
            var model = new MotorDetailsViewModel
            {
                Id = id,
                Make = motor.Make,
                Model = motor.Model,
                Year = motor.Specification.Year,
                HorsePower = motor.Specification.HorsePower,
                EngineDisplacement = motor.Specification.EngineDisplacement,
                TransmissionType = motor.Specification.TransmissionType,
                FuelType = motor.Specification.FuelType,
                Color = motor.Specification.Color,
                Condition = motor.Specification.Condition,
            };
            return View(model);
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
