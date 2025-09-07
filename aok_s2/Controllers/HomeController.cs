using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aok_s2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aok_s2.Areas.Identity.Data;

namespace aok_s2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly aok_s2IdentityDbContext _context;

    public HomeController(ILogger<HomeController> logger, aok_s2IdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        
        ViewBag.Semesters = new SelectList(_context.Classes.Select(c => c.Semester).Distinct().OrderBy(x => x));
        ViewBag.ClassFormations = new SelectList(_context.Classes.Select(c => c.ClassFormation).Distinct().OrderBy(x => x));
        ViewBag.Departments     = new SelectList(_context.Classes.Select(c => c.Department).Distinct().OrderBy(x => x));
        ViewBag.Majors          = new SelectList(_context.Majors.OrderBy(m => m.MajorName), "Id", "MajorName");
        return View();
    }

    public async Task<IActionResult> ResultAsync(string semester, string classFormation, string department, int? majorId, string className)
    {
        var startTime = DateTime.Now;
       var classQuery = _context.Classes.AsQueryable();

        if (!string.IsNullOrEmpty(semester))
            classQuery = classQuery.Where(c => c.Semester == semester);

        if (!string.IsNullOrEmpty(classFormation))
            classQuery = classQuery.Where(c => c.ClassFormation == classFormation);

        if (!string.IsNullOrEmpty(department))
            classQuery = classQuery.Where(c => c.Department == department);

        if (majorId.HasValue)
            classQuery = classQuery.Where(c => c.ClassMajors.Any(cm => cm.MajorId == majorId.Value));

        if (!string.IsNullOrEmpty(className))
            classQuery = classQuery.Where(c => c.ClassName.Contains(className));

        var list = await classQuery.ToListAsync();

        var endTime = DateTime.Now;
        var elapsed = (endTime - startTime).TotalMilliseconds;
        Console.WriteLine($"検索時間: {elapsed} ms");
        
        return View(list);
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
