using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using NVMotors.Web.ViewModels;
using System.Drawing;

namespace NVMotors.Web.Controllers
{
    public class VechicleController : Controller
    {
        private readonly NVMotorsDbContext context;
        public VechicleController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    var model = new VechicleAddViewModel();
           
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add(VechicleAddViewModel modelAdd)
        //{
           
           
        //    context.Specifications.Add(specification);
        //    context.Vechicles.Add(vechicle);
        //    await context.SaveChangesAsync();

        //    return View();
        //}
        

    }
}
