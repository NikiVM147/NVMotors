using Microsoft.EntityFrameworkCore;
using NVMotors.Data.Models;
using NVMotors.Data;
using NVMotors.Sevices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using NVMotors.Web.ViewModels.AdImage;

namespace NVMotors.Tests
{
    public class AdImageServiceTests
    {
        private NVMotorsDbContext context;
        private AdImageService adImageService;
        private Guid adId;
        private MotorImage motorImage;
        private AdImage adImage;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NVMotorsDbContext>()
                .UseInMemoryDatabase(databaseName: "NVMotors")
            .Options;

            context = new NVMotorsDbContext(options);
            adImageService = new AdImageService(context);

            var ad = new Ad
            {
                Id = Guid.NewGuid(),
                MotorId = Guid.NewGuid(),
                Description = "Test Ad",
                Price = 5000.00M,
                Town = "Test Town",
                PhoneNumber = "1234567890"
            };
            motorImage = new MotorImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = "/images/testimage.jpg"
            };
            adImage = new AdImage
            {
                AdId = ad.Id,
                ImageId = motorImage.Id,
                Image = motorImage
            };

            context.Ads.Add(ad);
            context.MotorImages.Add(motorImage);
            context.AdsImages.Add(adImage);

            context.SaveChanges();

            adId = ad.Id;
        }

        [Test]
        public async Task AddImagesAsyncShouldWork() 
        {
            var files = new FormFileCollection
            {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Mock image 1")), 0, 100, "Data", "image1.jpg"),
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Mock image 2")), 0, 100, "Data", "image2.jpg")
            };
            var model = new CreateAdImagesViewModel()
            {
                AdId = adId,
                Images = files
            };
            var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }
            await adImageService.AddImagesAsync(model);
            var images = await context.AdsImages.Where(ai => ai.AdId == adId).ToListAsync();
            Assert.That(images, Is.Not.Null);
            Assert.That(images.Count(), Is.EqualTo(3));
        }
        [Test]
        public async Task DeleteImageAsyncShouldWork()
        {
            await adImageService.DeleteImageAsync(motorImage.Id, adId);
            Assert.That(context.MotorImages.Count(), Is.EqualTo(0));
            Assert.That(context.AdsImages.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task ManageImagesAsyncShouldWork()
        {
            var model = await adImageService.ManageImagesAsync(adId);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ExistingImages.Count(), Is.EqualTo(1));
        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
