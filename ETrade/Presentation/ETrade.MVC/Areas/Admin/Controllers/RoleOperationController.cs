using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class RoleOperationController : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
    }
