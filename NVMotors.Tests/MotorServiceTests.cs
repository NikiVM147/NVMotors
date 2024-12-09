using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Services.Data;
using NVMotors.Web.ViewModels.Motor;
using System.Threading.Tasks;

namespace NVMotors.Tests
{
    public class MotorServiceTests
    {
        private NVMotorsDbContext context;
        private MotorService motorService;
        private Guid motorId;
        private Motor motor;
        private Guid userId;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NVMotorsDbContext>()
           .UseInMemoryDatabase(databaseName: "NVMotors")
           .Options;
            context = new NVMotorsDbContext(options);
            motorService = new MotorService(context);
            motorId = Guid.NewGuid();
            userId = Guid.NewGuid();
            motor = new Motor
            {
                Id = motorId,
                Make = "Yamaha",
                Model = "MT-07",
                Specification = new Specification
                {
                    Year = 2021,
                    HorsePower = 74,
                    EngineDisplacement = 689,
                    TransmissionType = "Manual",
                    FuelType = "Petrol",
                    Color = "Blue",
                    Condition = "New",
                   
                },
                MotorCategory =  new MotorCategory {
                    Name = "ATV"
                },
                IsDeleted = false,
                SellerId = userId,
            };

            context.Motors.Add(motor);
            context.SaveChanges();
        }

        [Test]
        public async Task CreateMotorAsyncShouldWork()
        {
            var addModel = new MotorAddViewModel
            {
                Make = "Honda",
                Model = "CB500X",
                Year = 2022,
                HorsePower = 47,
                EngineDisplacement = 500,
                SelectedTransmissionType = TransmissionType.Manual,
                SelectedFuelType = FuelType.Petrol,
                SelectedColor = MotorColor.Black,
                SelectedCondition = Condition.New,
                CategoryId = Guid.NewGuid() 
            };
            var userId = Guid.NewGuid();
            await motorService.CreateMotorAsync(addModel, userId);
            var motor = await context.Motors.FirstOrDefaultAsync(m => m.Model == addModel.Model);
            Assert.That(motor, Is.Not.Null);
            Assert.That(context.Motors.Count(), Is.EqualTo(2));
            Assert.That(motor.Specification.TransmissionType, Is.EqualTo("Manual"));
        }
        [Test]
        public async Task CreateMotorAsyncShouldThrowExeptionWhenModelNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => motorService.CreateMotorAsync(null, Guid.NewGuid()));
        }
        [Test]
        public async Task DeleteMotorAsyncShouldWork()
        {
            var model = new MotorIndexViewModel()
            {
                Id = motor.Id,
                Make = motor.Make,
                Model = motor.Model
            };
            await motorService.DeleteMotorAsync(model);
            var motorFound = await context.Motors.FirstOrDefaultAsync(m => m.Id == motorId);
            Assert.That(motorFound.IsDeleted, Is.True);
        }
        [Test]
        public async Task DetailsMotorAsyncShouldWork()
        {
            var result = await motorService.DetailsMotorAsync(motorId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.FuelType, Is.EqualTo("Petrol"));
            Assert.That(result.Color, Is.EqualTo("Blue"));
        }
        [Test]
        public async Task EditMotorAsyncShouldWork()
        {
            var editModel = new MotorAddViewModel
            {
                Id = motorId,
                Make = "Yamaha",
                Model = "MT-09",
                Year = 2023,
                HorsePower = 117,
                EngineDisplacement = 890,
                SelectedTransmissionType = TransmissionType.Manual,
                SelectedFuelType = FuelType.Petrol,
                SelectedColor = MotorColor.Red,
                SelectedCondition = Condition.Used,
                CategoryId = motor.MotorCategory.Id
            };
            await motorService.EditMotorAsync(editModel);
            var motorFound = await context.Motors.FirstOrDefaultAsync(m => m.Id == motor.Id);
            Assert.That(motorFound, Is.Not.Null);
            Assert.That(motorFound.Specification.Color, Is.EqualTo("Red"));

        }
        [Test]
        public async Task GetAllMotorsForCurrentUserAsyncShouldWork()
        {
            var result = await motorService.GetAllMotorsForCurrentUserAsync(userId);
            Assert.That(result.Count(), Is.EqualTo(1));
        }
            [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}