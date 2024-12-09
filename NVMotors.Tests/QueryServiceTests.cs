using Microsoft.EntityFrameworkCore;
using NVMotors.Data.Models;
using NVMotors.Data;
using NVMotors.Sevices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Query;

namespace NVMotors.Tests
{
    public class QueryServiceTests
    {
        private NVMotorsDbContext context;
        private QueryService queryService;
        private Guid motorId;
        private Guid adId;
        private Guid userId;
        private Guid userIdRequestor;
        private Ad ad;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NVMotorsDbContext>()
                .UseInMemoryDatabase(databaseName: "NVMotors")
            .Options;

            context = new NVMotorsDbContext(options);
            queryService = new QueryService(context);

            motorId = Guid.NewGuid();
            adId = Guid.NewGuid();
            userId = Guid.NewGuid();
            userIdRequestor = Guid.NewGuid();
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
                IsApproved = true,
            };
            context.Motors.Add(motor);
            context.Ads.Add(ad);
            var user = new AppUser
            {
                Id = userIdRequestor,
                Email = "test@test",
                FirstName = "test",
                LastName = "test"
            };
            context.Users.Add(user);
            context.Queries.Add(new Query
            {
                AdId = adId,
                RequesterId = userIdRequestor,
                PhoneNumber = "123456789",
                Description = "test",
                DateRequested = DateTime.Now,
                Requester = user
            });
            context.SaveChanges();
        }
        [Test]
        public async Task CreateQueryAsyncShouldWork()
        {
            var userIdRequestor = Guid.NewGuid();
            var queryModel = new MakeQueryViewModel
            {
                AdId = adId,
                PhoneNumber = "123456789",
                Description = "test"
            };
            await queryService.CreateQueryAsync(queryModel, userIdRequestor);
            Assert.That(context.Queries.Count(), Is.EqualTo(2));
        }
        [Test]
        public async Task IndexGetMyRequestsAsyncShouldWork()
        {
            var model = await queryService.IndexGetMyRequestsAsync(userIdRequestor);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task IndexGetReceivedRequestsAsyncShouldWork()
        {
            var model = await queryService.IndexGetReceivedRequestsAsync(userId);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(1));
        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
