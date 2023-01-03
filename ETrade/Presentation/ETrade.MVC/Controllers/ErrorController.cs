using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ETrade.MVC.Controllers;

[AllowAnonymous]
[DisableRateLimiting]
public class ErrorController : Controller
{
    public IActionResult Index(int statusCode,string errorTitle=null, string errorMessage=null)
    {
        switch (statusCode)
        {
            case 001:
                ViewBag.StatusCode = 001;
                ViewBag.ErrorTitle = errorTitle ?? "Oh no!";
                ViewBag.ErrorMessage = errorMessage?? "Bağlantının süresi doldu.";
                break;            
            case 400:
                ViewBag.StatusCode = 400;
                ViewBag.ErrorTitle = errorTitle ?? "Bad Request";
                ViewBag.ErrorMessage = errorMessage ?? "Geçersiz istek.";
                break;
            case 401:
                ViewBag.StatusCode = 401;
                ViewBag.ErrorTitle = errorTitle ?? "Access Denied";
                ViewBag.ErrorMessage = errorMessage ?? "Bu sayfaya girebilmek için yetkiniz bulunmamaktadır.";
                break;
            case 403:
                ViewBag.StatusCode = 403;
                ViewBag.ErrorTitle = errorTitle ?? "Forbidden";
                ViewBag.ErrorMessage = errorMessage ?? "Yasaklandı";
                break;
            case 404:
                ViewBag.StatusCode = 404;
                ViewBag.ErrorTitle = errorTitle ?? "Not Found";
                ViewBag.ErrorMessage = errorMessage ?? "Sayfa Bulunamadı";
                break;
            case 405:
                ViewBag.StatusCode = 405;
                ViewBag.ErrorTitle = errorTitle ?? "Method Not Allowed";
                ViewBag.ErrorMessage = errorMessage ?? "İzin verilmeyen Metod";
                break;
            case 429:
                ViewBag.StatusCode = 429;
                ViewBag.ErrorTitle = errorTitle ?? WebUtility.UrlDecode(errorTitle);
                ViewBag.ErrorMessage = errorMessage ?? WebUtility.UrlDecode(errorMessage);
                break;
            case 500:
                ViewBag.StatusCode = 500;
                ViewBag.ErrorTitle = errorTitle ?? "Internal Server Error";
                ViewBag.ErrorMessage = errorMessage ?? "Sunucu Hatası";
                break;
            case 503:
                ViewBag.StatusCode = 503;
                ViewBag.ErrorTitle = errorTitle ?? "Service Unavailable";
                ViewBag.ErrorMessage = errorMessage ?? "Hizmet kullanılamıyor";
                break;
            case 504:
                ViewBag.StatusCode = 504;
                ViewBag.ErrorTitle = errorTitle ?? "Gateway Timeout";
                ViewBag.ErrorMessage = errorMessage ?? "Ağ Geçidi Zaman Aşımı";
                break;
            default:
                ViewBag.StatusCode = statusCode;
                ViewBag.ErrorTitle = errorTitle ?? "Oh no!";
                ViewBag.ErrorMessage = errorMessage ?? "Hata Oluştu";
                break;
        }
        return View();
    }
}