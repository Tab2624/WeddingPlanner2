using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace WeddingPlanner2.Controllers;

public class WeddingsController : Controller
{
    private readonly ILogger<WeddingsController> _logger;
    private MyContext _context;

    public WeddingsController(ILogger<WeddingsController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
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

    [SessionCheck]
    [HttpGet("/weddings")]
    public IActionResult Weddings()
    {
        List<Wedding> WeddingsFromDb = _context.Weddings.Include(p => p.Creator).Include(p => p.UserRSVPs).OrderByDescending(p => p.CreatedAt).ToList();

        return View("Weddings", WeddingsFromDb);
    }

    [HttpGet("/weddings/add")]
    public IActionResult AddWedding()
    {
        return View("PlanWedding");
    }

    [HttpPost("/weddings/create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
        if (!ModelState.IsValid)
        {
            System.Console.WriteLine("Verification failed");
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    System.Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }

            return View("PlanWedding");
        }
        System.Console.WriteLine("Verification success");
        newWedding.UserId = (int)HttpContext.Session.GetInt32("UserId");

        _context.Weddings.Add(newWedding);
        // _context.Add(newWedding);
        //! SAVE CHANGES TO THE DB, OR IT WON'T BE PERMANENT!
        _context.SaveChanges();

        // , new { WeddingId = newWedding.WeddingId }
        // return RedirectToAction("AllWeddings");
        // return Redirect($"/Weddings/{newWedding.WeddingId}");
        return RedirectToAction("Weddings");
    }

    // Delete by ID
    [HttpPost("weddings/{weddingId}/delete")]
    public RedirectToActionResult DeleteWedding(int weddingId)
    {
        System.Console.WriteLine(weddingId);
        Wedding? weddingToDelete = _context.Weddings.SingleOrDefault(d => d.WeddingId == weddingId);
        if (weddingToDelete != null)
        {
            _context.Remove(weddingToDelete);

            // Save Changes
            _context.SaveChanges();
        }
        return RedirectToAction("Weddings");
    }

    [HttpGet("/weddings/{weddingId}")]
    public IActionResult ViewWedding(int weddingId)
    {
        Wedding? OneWedding = _context.Weddings
                                    .Include(w => w.UserRSVPs)
                                    .ThenInclude(w => w.RSVPUser)
                                    .Include(w => w.Creator)
                                    .FirstOrDefault(w => w.WeddingId == weddingId);

        if (OneWedding == null)
        {
            return RedirectToAction("Weddings");
        }

        return View("ViewOne", OneWedding);
    }
}
