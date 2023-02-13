using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TagHelperDemo.Shared.PaginationTagHelper;
using TagHelperDemo.WebApp.Data;
using TagHelperDemo.WebApp.Domain;
using TagHelperDemo.WebApp.Models;

namespace TagHelperDemo.WebApp.Controllers;

public class FamilyNamesController : Controller
{
    private readonly ILogger<FamilyNamesController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public FamilyNamesController(ILogger<FamilyNamesController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index(int pageSize = 25, int pageNumber = 1)
    {
        var familyNames = _dbContext.FamilyNames!
            .OrderByDescending(x => x.OccurenceCount);
        var pagedList = PagedList<FamilyName>.Create(familyNames, pageNumber, pageSize);
        return View(pagedList);
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