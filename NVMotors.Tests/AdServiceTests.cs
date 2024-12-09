using Microsoft.EntityFrameworkCore;
using NVMotors.Data.Models;
using NVMotors.Data;
using NVMotors.Sevices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVMotors.Web.ViewModels.Ad;

namespace NVMotors.Tests
{
    public class AdServiceTests
    {
        private NVMotorsDbContext context;
        private AdService adService;
        private Guid motorId; 

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NVMotorsDbContext>()
                .UseInMemoryDatabase(databaseName: "NVMotors") 
            .Options;

            context = new NVMotorsDbContext(options);
            adService = new AdService(context);

            motorId = Guid.NewGuid();
            var motor = new Motor
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
                    Condition = "New"
                },
                MotorCategory = new MotorCategory
                {
                    Name = "ATV"
                },
                IsDeleted = false
            };

            context.Motors.Add(motor);
            context.SaveChanges();
        }
        [Test]
        public async Task CreateAdAsyncShouldWork()
        {
            var adModel = new CreateAdViewModel
            {
                MotorModelId = motorId,
                Description = "Test Ad",
                Price = 2000,
                Town = "Test Town",
                PhoneNumber = "1234567890"
            };
            var adId = await adService.CreateAdAsync(adModel);
            var adFound = await context.Ads.FindAsync(adId);
            Assert.That(adFound.MotorId, Is.EqualTo(motorId));
        }



        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
