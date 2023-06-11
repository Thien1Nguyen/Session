using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Session.Models;

namespace Session.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("login")]
    public IActionResult Login(string UserName)
    {
        HttpContext.Session.SetString("UserName", UserName);
        HttpContext.Session.SetInt32("UserNum", 42);
        return Redirect("/dashboard");
    }


    [HttpPost("Math")]
    public IActionResult Math(MathSwitch newMath)
    {
        HttpContext.Session.SetString("MathState", newMath.State);

        string? MathState = HttpContext.Session.GetString("MathState");

        int? UserNum = HttpContext.Session.GetInt32("UserNum");

        Random rand = new Random();

        if(MathState == "plus")
        {
            HttpContext.Session.SetInt32("UserNum", (int)UserNum + 1);
        }
        else if (MathState == "minus")
        {
            HttpContext.Session.SetInt32("UserNum", (int)UserNum - 1);
        }
        else if(MathState == "time2")
        {
            HttpContext.Session.SetInt32("UserNum", (int)UserNum * 2);
        }
        else if (MathState == "rand")
        {
            HttpContext.Session.SetInt32("UserNum", (int)UserNum + rand.Next(1,11));
        }
        return Redirect("/dashboard");
    }

    [HttpGet("dashboard")]
    public IActionResult DashBoard()
    {
        string? LoginCheck =  HttpContext.Session.GetString("UserName");
        if(LoginCheck != null)
        {
            return View();
        }

        return Redirect("/");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/");
    }


    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
