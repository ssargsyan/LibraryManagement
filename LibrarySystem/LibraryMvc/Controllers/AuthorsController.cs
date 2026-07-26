using LibraryMvc.Models;
using LibraryMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMvc.Controllers;

public class AuthorsController : Controller
{
    private readonly LibraryAuthorsApiClient _apiClient;

    public AuthorsController(LibraryAuthorsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _apiClient.GetAuthorsAsync();

        return View(authors);
    }

  






}