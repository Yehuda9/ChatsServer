using Microsoft.AspNetCore.Mvc;
using Rating_page.Models;
using Microsoft.AspNetCore.Authorization;



[Route("ratings")]
//[ApiController]
public class RatingsController : Controller
{
    private IRatingService service;
    public RatingsController(IRatingService ratingService)
    {
        this.service = ratingService;

    }
    public IActionResult Index()
    {
        return View(service.GetAll());
    }

    [HttpGet]
    [Route("Create")]
    public IActionResult Create(int Id)
    {
        return View();
    }

    [HttpPost]
    [Route("Create")]
    public IActionResult Create(string composer, string title, string description, int rating)
    {
        service.Create(composer, title, description, rating);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("Details")]
    public IActionResult Details(int Id)
    {
        return View(service.Get(Id));
    }

    [HttpGet]
    [Route("Edit")]
    public IActionResult Edit(int Id)
    {
        return View(service.Get(Id));
    }

    [HttpPost]
    [Route("Edit")]
    public IActionResult Edit(int Id, string composer, string title, string description, int rating)
    {
        service.Edit(Id, composer, title, description, rating);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("Delete")]
    public IActionResult Delete(int Id)
    {
        return View(service.Get(Id));
    }

    [HttpPost]
    [Route("Delete")]
    public IActionResult DeleteRating(int Id)
    {
        service.Delete(Id);
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [Route("Search")]
    public IActionResult Search(string searchText)
    {
        if (string.IsNullOrEmpty(searchText)) return RedirectToAction(nameof(Index));
        return View(service.SearchRating(searchText));
    }
}

