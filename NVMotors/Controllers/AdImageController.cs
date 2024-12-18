﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using NVMotors.Web.ViewModels.AdImage;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class AdImageController : Controller
    {
        private readonly IAdImageService adImageService;
        public AdImageController( IAdImageService _adImageService)
        {
            adImageService = _adImageService;
        }
        [HttpGet]
        public async Task<IActionResult> AddImages(Guid id)
        {
            var model = new CreateAdImagesViewModel
            {
                AdId = id, 
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddImages(CreateAdImagesViewModel imageModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(imageModel);
                }

                await adImageService.AddImagesAsync(imageModel);
                TempData[nameof(SuccessData)] = "Successfull! Pending approval by Admin";
                return RedirectToAction("IndexAds", "Ad");
            }
            catch (Exception ex) when (ex is ArgumentNullException)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Motor");
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> ManageImages(Guid id)
        {
            try
            {
                var model = await adImageService.ManageImagesAsync(id);

                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("IndexAds", "Ad");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid imageId, Guid adId)
        {
            try
            {
                await adImageService.DeleteImageAsync(imageId, adId);
                TempData["Success"] = "Image deleted successfully.";
                return RedirectToAction(nameof(ManageImages), new { id = adId });
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("IndexAds", "Ad");
            }

        }
        [HttpPost]
        public IActionResult SetSuccessMessage()
        {
            TempData[nameof(SuccessData)] = "Successfull! Pending approval by Admin";
            return RedirectToAction("IndexAds","Ad");
        }

    }
}
