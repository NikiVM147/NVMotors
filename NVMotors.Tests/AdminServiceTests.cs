using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data;
using NVMotors.Sevices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Tests
{
    public class AdminServiceTests
    {
        private NVMotorsDbContext context;
        private AdminService adminService;
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
            adminService = new AdminService(context);

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
                PhoneNumber = "1234567890",
                IsApproved = false,
            };
            context.Motors.Add(motor);
            context.Ads.Add(ad);
            context.SaveChanges();
        }

        [Test]
        public async Task IndexGetAllAdsToBeApprovedShoulWork()
        {
            var model = await adminService.IndexGetAllAdsToBeApproved();
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task ApproveAdAsyncShouldWork()
        {
            await adminService.ApproveAdAsync(adId);
            var adFound = await context.Ads.FirstOrDefaultAsync(a => a.Id == adId);
            Assert.That(adFound, Is.Not.Null);
            Assert.That(adFound.IsApproved, Is.True);
        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
