using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.MVC.Controllers;

[AllowAnonymous]
public class ErrorInfoController : Controller
{
    public IActionResult ErrorPage(int statusCode)
    {
        switch (statusCode)
        {
            case 001:
                ViewBag.StatusCode = 001;
                ViewBag.ErrorTitle = "Oh no!";
                ViewBag.ErrorMessage = "Bağlantının süresi doldu.";
                break;            
            case 400:
                ViewBag.StatusCode = 400;
                ViewBag.ErrorTitle = "Bad Request";
                ViewBag.ErrorMessage = "Geçersiz istek.";
                break;
            case 401:
                ViewBag.StatusCode = 401;
                ViewBag.ErrorTitle = "Access Denied";
                ViewBag.ErrorMessage = "Bu sayfaya girebilmek için yetkiniz bulunmamaktadır.";
                break;
            case 403:
                ViewBag.StatusCode = 403;
                ViewBag.ErrorTitle = "Forbidden";
                ViewBag.ErrorMessage = "Yasaklandı";
                break;
            case 404:
                ViewBag.StatusCode = 404;
                ViewBag.ErrorTitle = "Not Found";
                ViewBag.ErrorMessage = "Sayfa Bulunamadı";
                break;
            case 405:
                ViewBag.StatusCode = 405;
                ViewBag.ErrorTitle = "Method Not Allowed";
                ViewBag.ErrorMessage = "İzin verilmeyen Metod";
                break;
            case 500:
                ViewBag.StatusCode = 500;
                ViewBag.ErrorTitle = "Internal Server Error";
                ViewBag.ErrorMessage = "Sunucu Hatası";
                break;
            case 503:
                ViewBag.StatusCode = 503;
                ViewBag.ErrorTitle = "Service Unavailable";
                ViewBag.ErrorMessage = "Hizmet kullanılamıyor";
                break;
            case 504:
                ViewBag.StatusCode = 504;
                ViewBag.ErrorTitle = "Gateway Timeout";
                ViewBag.ErrorMessage = "Ağ Geçidi Zaman Aşımı";
                break;
            default:
                ViewBag.StatusCode = statusCode;
                ViewBag.ErrorTitle = "Oh no!";
                ViewBag.ErrorMessage = "Hata Oluştu";
                break;
        }

        return View();
    }

    public IActionResult QuotaExceededRateLimit(int statusCode, string errorTitle, string errorMessage)
    {
        ViewBag.StatusCode = statusCode;
        ViewBag.ErrorTitle = WebUtility.UrlDecode(errorTitle);
        ViewBag.ErrorMessage = WebUtility.UrlDecode(errorMessage);
        return View();
    }
    
}