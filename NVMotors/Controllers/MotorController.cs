using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Motor;
using System.Security.Claims;
using static NVMotors.Common.Constants;
using Microsoft.VisualBasic;

namespace NVMotors.Web.Controllers
{
    //TODO categories, validations
    [Authorize(Roles = "User")]
    public class MotorController : Controller
    {
        private readonly IMotorService motorService;
        private readonly NVMotorsDbContext context;
        public MotorController(IMotorService _motorService, NVMotorsDbContext _context)
        {
            motorService = _motorService;
            context = _context; 
        }
        public Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUserID = GetCurrentUserId();
                var model = await motorService.GetAllMotorsForCurrentUserAsync(currentUserID);
                return View(model);
            }
            catch (ArgumentException ex)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await motorService.LoadMotorViewModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MotorAddViewModel addModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    addModel = await motorService.LoadMotorViewModel();

                    return View(addModel);
                }
                TempData[nameof(SuccessData)] = "Successfully created motor!";
                await motorService.CreateMotorAsync(addModel, GetCurrentUserId());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Create));
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = await motorService.DetailsMotorAsync(id);
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await motorService.LoadEditModelAsync(id);
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(MotorAddViewModel editModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    editModel = await motorService.LoadMotorViewModel();

                    return View(editModel);
                }
                await motorService.EditMotorAsync(editModel);
                TempData[nameof(SuccessData)] = "Successfully edited motor!";
                return RedirectToAction(nameof(Details), new { id = editModel.Id });
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model = await motorService.GetDeleteMotorModelAsync(id);
                return View(model);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(MotorIndexViewModel deleteModel)
        {
            try
            {
                await motorService.DeleteMotorAsync(deleteModel);
                TempData[nameof(SuccessData)] = "Successfully deleted motor!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }
        }
}
