using Microsoft.AspNetCore.Mvc;
using NVMotors.Sevices.Data;
using NVMotors.Sevices.Data.Interfaces;

namespace NVMotors.Web.Controllers
{
    public class AdminController : Controller
    { private readonly IAdminService adminService;

        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        [HttpGet]
        public async Task<IActionResult> Approve()
        {
            var model = await adminService.IndexGetAllAdsToBeApproved();
            ViewBag.ShowApproveButton = true;
            return View("Approve", model);
        }
        public async Task<IActionResult> ApproveAd(Guid id) 
        {
            await adminService.ApproveAdAsync(id);
            return RedirectToAction(nameof(Approve));
        }
    }
}
