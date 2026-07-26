using LibraryMvc.Models;
using LibraryMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMvc.Controllers;

public class BooksController : Controller
{
    private readonly LibraryBooksApiClient _apiClient;

    public BooksController(LibraryBooksApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _apiClient.GetBooksAsync();

        return View(books);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int bookId)
    {
        await _apiClient.RemoveBookAsync(bookId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Borrow(int borrowBookId)
    {
        Console.WriteLine("------------------");
        await _apiClient.ChangeBorrowStatusAsync(borrowBookId, true);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Return(int returnBookId)
    {
        await _apiClient.ChangeBorrowStatusAsync(returnBookId, false);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new BookFormViewModel
        {
            Id = 0,
            Title = "",
            Author = ""
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookViewModel model)
    {
        if (ModelState.IsValid)
        {
            // save database

            await _apiClient.CreateBookAsync(new CreateBookRequest { Title = model.Title, Author = model.Author });
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _apiClient.GetBookAsync(id);
        if (book == null) { NotFound(); }
        return View(new BookFormViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookFormViewModel model)
    {
        if (ModelState.IsValid)
        {
            // save database

            await _apiClient.UpdateBookAsync(new UpdateBookRequest { Id = model.Id, Title = model.Title, Author = model.Author });
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}