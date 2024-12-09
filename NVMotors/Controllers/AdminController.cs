using Microsoft.AspNetCore.Mvc;
using NVMotors.Sevices.Data;
using NVMotors.Sevices.Data.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            try
            {
                await adminService.ApproveAdAsync(id);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException || ex is NullReferenceException)
            {
                TempData[nameof(Error)] = ex.Message;
            }
            return RedirectToAction(nameof(Approve));

        }
    }
}
