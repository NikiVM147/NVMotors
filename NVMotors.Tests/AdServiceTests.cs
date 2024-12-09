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
        private Guid adId;
        private Guid userId;
        private Ad ad;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NVMotorsDbContext>()
                .UseInMemoryDatabase(databaseName: "NVMotors") 
            .Options;

            context = new NVMotorsDbContext(options);
            adService = new AdService(context);

            motorId = Guid.NewGuid();
            adId = Guid.NewGuid();
            userId = Guid.NewGuid();
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
                IsDeleted = false,
                SellerId = userId,
            };
            ad = new Ad
            {
                Id = adId,
                MotorId = motorId,
                Description = "Ad Details",
                Price = 2500,
                Town = "Test Town",
                PhoneNumber = "1234567890"
            };
            context.Motors.Add(motor);
            context.Ads.Add(ad);
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

        [Test]
        public async Task GetAdDetailsAsyncShouldWork()
        {
            var model = await adService.GetAdDetailsAsync(adId, userId);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Town, Is.EqualTo("Test Town"));
        }
        [Test]
        public async Task IndexGetAllAds_ShouldReturnFilteredAds()
        {
            var filters = new AdFilterViewModel
            {
                MinPrice = 3000
            };
            var result = await adService.IndexGetAllAds(filters, null, 1, 12);

            Assert.That(result.Ads.Count, Is.EqualTo(0));
        }
        [Test]
        public async Task DeleteAdShouldWork()
        {
            await adService.DeleteAdAsync(adId);
            Assert.That(context.Ads.Count, Is.EqualTo(0));
        }
        [Test]
        public async Task EditAdShouldWork()
        {
            var editModel = new CreateAdViewModel
            {
                Id = ad.Id,
                MotorModelId = motorId,
                Description = "Updated Description",
                Price = 1800,
                Town = "Updated Town",
                PhoneNumber = "0987654321"
            };
            var adId = await adService.EditAdAsync(editModel);
            var adFound = await context.Ads.FirstOrDefaultAsync(a => a.Id == adId);
            Assert.That(adFound.Description, Is.EqualTo(editModel.Description));
            Assert.That(adFound.Town, Is.EqualTo(editModel.Town));
        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
