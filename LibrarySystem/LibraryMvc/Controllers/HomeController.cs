using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryMvc.Models;
using LibraryMvc.Services;
using System.Threading.Tasks;

namespace LibraryMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly LibraryBooksApiClient _booksApiClient;

    private readonly LibraryAuthorsApiClient _authorsApiClient;

    public HomeController(ILogger<HomeController> logger, LibraryBooksApiClient booksApiClient, LibraryAuthorsApiClient authorsApiClient)
    {
        _logger = logger;
        _booksApiClient = booksApiClient;
        _authorsApiClient = authorsApiClient;
    }

    public async Task<IActionResult> Index()
    {
        var booksCount = await _booksApiClient.GetBooksCountAsync();
        var borrowedBooksCount = await _booksApiClient.GetBorrowedBooksCountAsync();
        var availableBooksCount = await _booksApiClient.GetAvailableBooksCountAsync();
        var authorsCount = await _authorsApiClient.GetAuthorsCountAsync();
        return View(new HomeViewModel
        {
            TotalBooks = Convert.ToInt32(booksCount),
            TotalBorrowedBooks = Convert.ToInt32(borrowedBooksCount),
            TotalAvailableBooks = Convert.ToInt32(availableBooksCount),
            TotalAuthors = Convert.ToInt32(authorsCount)
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
