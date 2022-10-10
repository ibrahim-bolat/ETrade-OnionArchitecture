using Microsoft.AspNetCore.Mvc;


namespace ETrade.MVC.Areas.Admin.Controllers;

[Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
