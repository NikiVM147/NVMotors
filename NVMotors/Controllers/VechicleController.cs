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
            var model = new VechicleAddViewModel();
            model.FuelTypes = LoadFuelTypes();
            model.Colors = LoadColors();
            model.Conditions = LoadConditions();
            model.TransmissionTypes = LoadTransmissions();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(VechicleAddViewModel modelAdd)
        {
           
            if (!ModelState.IsValid)
            {
                modelAdd.FuelTypes = LoadFuelTypes();
                modelAdd.Colors = LoadColors();
                modelAdd.Conditions = LoadConditions();
                modelAdd.TransmissionTypes = LoadTransmissions();
                return View(modelAdd);
            }
            Specification specification = new Specification 
            {
                Year = modelAdd.Year,
                HorsePower = modelAdd.HorsePower,
                TransmissionType = modelAdd.SelectedTransmissionType,
                FuelType = modelAdd.SelectedFuelType,
                Color = modelAdd.SelectedColor,
                Condition = modelAdd.SelectedCondition,
            };
            Vechicle vechicle = new Vechicle
            {
                Make = modelAdd.Make,
                Model = modelAdd.Model,
                Specification = specification,
            };
            context.Specifications.Add(specification);
            context.Vechicles.Add(vechicle);
            await context.SaveChangesAsync();

            return View();
        }
        private IEnumerable<SelectListItem> LoadFuelTypes() 
        {
            return Enum.GetValues(typeof(FuelType))
                    .Cast<FuelType>()
                    .Select(fuelType => new SelectListItem
                    {
                        Value = fuelType.ToString(),
                        Text = fuelType == FuelType.None ? "<-- Choose Type -->" : fuelType.ToString(),
                        Disabled = fuelType == FuelType.None
                    })
                    .ToList();
        }
        private IEnumerable<SelectListItem> LoadColors()
        {
            return Enum.GetValues(typeof(VechicleColor))
                        .Cast<VechicleColor>()
                        .Select(color => new SelectListItem
                        {
                            Value = color.ToString(),
                            Text = color == VechicleColor.None ? "<-- Choose Color -->" : color.ToString(),
                            Disabled = color == VechicleColor.None
                        })
                        .ToList();
        }
        private IEnumerable<SelectListItem> LoadConditions()
        {
            return Enum.GetValues(typeof(Condition))
                        .Cast<Condition>()
                        .Select(condition => new SelectListItem
                        {
                            Value = condition.ToString(),
                            Text = condition == Condition.None ? "<-- Choose Condition -->" : condition.ToString(),
                            Disabled = condition == Condition.None
                        })
                        .ToList();
        }
        private IEnumerable<SelectListItem> LoadTransmissions()
        {
            return Enum.GetValues(typeof(TransmissionType))
                    .Cast<TransmissionType>()
                    .Select(type => new SelectListItem
                    {
                        Value = type.ToString(),
                        Text = type == TransmissionType.None ? "<-- Choose Transmission Type -->" : type.ToString(),
                        Disabled = type == TransmissionType.None
                    })
                    .ToList();
        }

    }
}
