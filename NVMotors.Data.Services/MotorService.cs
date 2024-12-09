using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Motor;
using System.Security.Claims;

namespace NVMotors.Services.Data
{
    public class MotorService : IMotorService
    {
        private readonly NVMotorsDbContext context;
        public MotorService(NVMotorsDbContext _context)
        {
            context = _context;
        }

        public async Task CreateMotorAsync(MotorAddViewModel addModel, Guid userId)
        {
            if (addModel == null) 
            {
                throw new ArgumentNullException(nameof(addModel));
            }
            if (userId == Guid.Empty) 
            {
                throw new ArgumentException("Invalid user ID.");
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
                SellerId = userId,
                MotorCategoryId = addModel.CategoryId,
            };

            await context.Specifications.AddAsync(specification);
            await context.Motors.AddAsync(motor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMotorAsync(MotorIndexViewModel deleteModel)
        {
            if (deleteModel == null) 
            {
                throw new ArgumentNullException("Error occured!");
            }
            var motor = await FindMotorByIdAsync(deleteModel.Id);
            motor.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task<MotorDetailsViewModel> DetailsMotorAsync(Guid id)
        {
            var motor = await FindMotorByIdAsync(id);
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
                Category = motor.MotorCategory.Name ?? string.Empty,
            };
            return model;
        }

        public async Task EditMotorAsync(MotorAddViewModel editModel)
        {
            if (editModel == null) 
            {
                throw new ArgumentNullException("Invalid data!");
            }
            var motor = await FindMotorByIdAsync(editModel.Id);

            motor.Make = editModel.Make;
            motor.Model = editModel.Model;
            motor.Specification.Year = editModel.Year;
            motor.Specification.HorsePower = editModel.HorsePower;
            motor.Specification.EngineDisplacement = editModel.EngineDisplacement;
            motor.Specification.TransmissionType = editModel.SelectedTransmissionType.ToString();
            motor.Specification.FuelType = editModel.SelectedFuelType.ToString();
            motor.Specification.Color = editModel.SelectedColor.ToString();
            motor.Specification.Condition = editModel.SelectedCondition.ToString();
            motor.MotorCategoryId = editModel.CategoryId;
           await context.SaveChangesAsync();
        }

        private async Task<Motor> FindMotorByIdAsync(Guid id)
        {
           var motor = await context.Motors.Include(m => m.Specification).Include(m => m.MotorCategory).Where(m => m.IsDeleted == false).FirstOrDefaultAsync(m => m.Id == id);
            if (motor == null) 
            {
                throw new NullReferenceException($"Motor not found.");
            }


            return motor;
        }

        public async Task<List<MotorIndexViewModel>> GetAllMotorsForCurrentUserAsync(Guid userId)
        {
            if (userId == Guid.Empty) 
            {
                throw new ArgumentException("Invalid user.");
            }
            return await context.Motors.Where(m => m.Seller.Id == userId)
               .Where(m => m.IsDeleted == false)
               .Select(m => new MotorIndexViewModel
               {
                   Id = m.Id,
                   Make = m.Make,
                   Model = m.Model,
                   Year = m.Specification.Year,
               }).ToListAsync();
        }

        public async Task<MotorIndexViewModel> GetDeleteMotorModelAsync(Guid id)
        {
            var motor = await FindMotorByIdAsync(id);
            var model = new MotorIndexViewModel
            {
                Id = id,
                Make = motor.Make,
                Model = motor.Model,
                Year = motor.Specification.Year,
            }; 
            return model;
        }

        private List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {
            return  Enum.GetValues(typeof(TEnum))
                     .Cast<TEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString() == "None" ? "Choose type" : e.ToString(),
                         Disabled = e.ToString() == "None"
                     })
                     .ToList();
        }

        private async Task<List<SelectListItem>> LoadCategoriesAsync()
        {
            return await context.MotorCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToListAsync();
        }

        public async Task<MotorAddViewModel> LoadEditModelAsync(Guid id)
        {
            var motor = await FindMotorByIdAsync(id);
            var model = new MotorAddViewModel()
            {
                Id = id,
                Make = motor.Make,
                Model = motor.Model,
                Year = motor.Specification.Year,
                HorsePower = motor.Specification.HorsePower,
                EngineDisplacement = motor.Specification.EngineDisplacement,
                SelectedTransmissionType = Enum.Parse<TransmissionType>(motor.Specification.TransmissionType),
                SelectedFuelType = Enum.Parse<FuelType>(motor.Specification.FuelType),
                SelectedColor = Enum.Parse<MotorColor>(motor.Specification.Color),
                SelectedCondition = Enum.Parse<Condition>(motor.Specification.Condition),
                TransmissionTypes = GetEnumSelectList<TransmissionType>(),
                FuelTypes = GetEnumSelectList<FuelType>(),
                MotorColors = GetEnumSelectList<MotorColor>(),
                Conditions = GetEnumSelectList<Condition>(),
                Categories = await LoadCategoriesAsync(),
                CategoryId = motor.MotorCategoryId,
            };
            return model;
        }

        public async Task<MotorAddViewModel> LoadMotorViewModel()
        {
            return new MotorAddViewModel
            {
                TransmissionTypes = GetEnumSelectList<TransmissionType>(),
                FuelTypes = GetEnumSelectList<FuelType>(),
                MotorColors = GetEnumSelectList<MotorColor>(),
                Conditions = GetEnumSelectList<Condition>(),
                Categories = await LoadCategoriesAsync(),
            };
        }
      
    }
}
