using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels;
using System.Security.Claims;

namespace NVMotors.Web.Controllers
{
    //TODO categories,ads, remove UserManeger
    [Authorize]
    public class MotorController : Controller
    {
        private readonly IMotorService motorService;
        public MotorController(IMotorService _motorService)
        {
            motorService = _motorService;
        }
        public Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserID = GetCurrentUserId();
            var model = await motorService.GetAllMotorsForCurrentUserAsync(currentUserID);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = motorService.LoadMotorViewModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MotorAddViewModel addModel)
        {
            if (!ModelState.IsValid)
            {
                addModel = motorService.LoadMotorViewModel();

                return View(addModel);
            }

            await motorService.CreateMotorAsync(addModel, GetCurrentUserId());
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await motorService.DetailsMotorAsync(id);
            return View(model);
        }
    }
}
