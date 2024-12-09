using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Query;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NVMotors.Common.Constants;
using NVMotors.Sevices.Data.Interfaces;

namespace NVMotors.Web.Controllers
{
    public class QueryController : BaseController
    {
        private readonly IQueryService queryService;
        public QueryController(IQueryService _queryService)
        {
            queryService = _queryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await queryService.IndexGetMyRequestsAsync(GetCurrentUserId());
                return View(model);
            }
            catch (ArgumentException ex)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> RequestsReceived()
        {
            try
            {
                var model = await queryService.IndexGetReceivedRequestsAsync(GetCurrentUserId());
                return View(model);
            }
            catch (ArgumentException ex)
            {
                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

        }
      
        [HttpPost]
        public async Task<IActionResult> MakeQuery(MakeQueryViewModel queryModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Details", "Ad", new { id = queryModel.AdId });
                }
                await queryService.CreateQueryAsync(queryModel, GetCurrentUserId());
                TempData[nameof(SuccessData)] = "Successfully made request!";
                return RedirectToAction("Index");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is InvalidOperationException)
            {

                TempData[nameof(ErrorData)] = ex.Message;
                return RedirectToAction("IndexAds", "Ad");
            }
           

        }
    }
}
