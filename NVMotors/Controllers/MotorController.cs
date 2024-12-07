﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Motor;
using System.Runtime.ExceptionServices;
using System.Security.Claims;

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
            var currentUserID = GetCurrentUserId();
            var model = await motorService.GetAllMotorsForCurrentUserAsync(currentUserID);
            return View(model);
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
            if (!ModelState.IsValid)
            {
                addModel = await motorService.LoadMotorViewModel();

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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var model = await motorService.LoadEditModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MotorAddViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                editModel = await motorService.LoadMotorViewModel();

                return View(editModel);
            }
            await motorService.EditMotorAsync(editModel);
            return RedirectToAction(nameof(Details), new {id = editModel.Id});
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await motorService.GetDeleteMotorModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(MotorIndexViewModel deleteModel)
        {
            await motorService.DeleteMotorAsync(deleteModel);
            return RedirectToAction(nameof(Index));
        }
        }
}
