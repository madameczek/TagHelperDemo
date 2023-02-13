using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TagHelperDemo.WebApp.Models;

namespace TagHelperDemo.WebApp.Controllers;

public class FamilyNamesController : Controller
{
    private readonly ILogger<FamilyNamesController> _logger;

    public FamilyNamesController(ILogger<FamilyNamesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}