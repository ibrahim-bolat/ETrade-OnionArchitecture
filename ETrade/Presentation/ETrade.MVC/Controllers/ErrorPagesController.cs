using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Controllers;

[AllowAnonymous]
public class ErrorPagesController : Controller
{
    public IActionResult AllErrorPages(int statusCode)
    {
        switch (statusCode)
        {
            case 001:
                ViewBag.ErrorMessage = "Bağlantının süresi doldu.";
                ViewBag.ErrorNumber = "Oh no!";
                break;            
            case 400:
                ViewBag.ErrorMessage = "Geçersiz istek.";
                ViewBag.ErrorNumber = "400";
                break;
            case 401:
                ViewBag.ErrorMessage = "Bu sayfaya erişiminiz yok. Lütfen oturum açtığınızdan emin olun.";
                ViewBag.ErrorNumber = "401";
                break;
            case 403:
                ViewBag.ErrorMessage = "Yasaklandı";
                ViewBag.ErrorNumber = "403";
                break;
            case 404:
                ViewBag.ErrorMessage = "Sayfa Bulunamadı";
                ViewBag.ErrorNumber = "404";
                break;
            case 405:
                ViewBag.ErrorMessage = "İzin verilmeyen Metod";
                ViewBag.ErrorNumber = "405";
                break;
            case 500:
                ViewBag.ErrorMessage = "Sunucu Hatası";
                ViewBag.ErrorNumber = "500";
                break;
            case 503:
                ViewBag.ErrorMessage = "Hizmet kullanılamıyor";
                ViewBag.ErrorNumber = "503";
                break;
            case 504:
                ViewBag.ErrorMessage = "Ağ Geçidi Zaman Aşımı";
                ViewBag.ErrorNumber = "504";
                break;
            default:
                ViewBag.ErrorMessage = "Hata Oluştu";
                ViewBag.ErrorNumber = $"{statusCode.ToString()}";
                break;
        }

        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}