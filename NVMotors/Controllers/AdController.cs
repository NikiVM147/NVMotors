using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.Controllers
{
    public class AdController : BaseController
    {
        private readonly IAdService adService;
        public AdController( IAdService _adService)
        {
            adService = _adService;
        }
        
        public async Task<IActionResult> IndexAds(AdFilterViewModel filters, string searchQuery, int page = 1, int pageSize = 12)
        {
            try
            {
                var model = await adService.IndexGetAllAds(filters, searchQuery, page, pageSize);
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAd(Guid id)
        {
            try
            {
                ViewData["Text"] = "Create Ad";
                var model = new CreateAdViewModel();
                if (await adService.ValidateMotorId(id) == true)
                {
                    model.MotorModelId = id;
                }
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Motor");
            }
           
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAd(CreateAdViewModel adModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(adModel);
                }
                var adId = await adService.CreateAdAsync(adModel);
                return RedirectToAction("AddImages", "AdImage", new { id = adId });
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Motor");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = await adService.GetAdDetailsAsync(id, GetCurrentUserId());
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(IndexAds));
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await adService.DeleteAdAsync(id);
                TempData[nameof(SuccessData)] = "Ad successfully deleted!";
                return RedirectToAction(nameof(IndexAds));
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(IndexAds));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, Guid motorId)
        {
            try
            {
                ViewData["Text"] = "Edit Ad";
                var model = await adService.GetEditViewModelAsync(id, motorId);
                return View("CreateAd", model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(IndexAds));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateAdViewModel editModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("CreateAd", editModel);
                }
                var adId = await adService.EditAdAsync(editModel);
                return RedirectToAction("ManageImages", "AdImage", new { id = adId });
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(IndexAds));
            }

        }

        }
}
