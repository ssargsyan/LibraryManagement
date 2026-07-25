using Microsoft.AspNetCore.Mvc;
using LibraryApi.Models;
using LibraryApi.Services;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryBooksService _libraryBooksService;
    private readonly ILibraryAuthorsService _libraryAuthorsService;
    public LibraryController(ILibraryBooksService booksService, ILibraryAuthorsService authorsService)
    {
        _libraryBooksService = booksService;
        _libraryAuthorsService = authorsService;

    }

    [HttpGet("books")]
    public IEnumerable<BookResponse> GetAllBooks(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
    {
        return _libraryBooksService.GetAllBooks(search, authorId, sortBy, page, pageSize);
    }

    [HttpGet("booksCount")]
    public int GetAllBooksCount(string? search, int? authorId, string? sortBy, string? IsBorrowed)
    {
        return _libraryBooksService.GetAllBooksCount(search, authorId, sortBy, IsBorrowed);
    }

    [HttpGet("authors")]
    public IEnumerable<Author> GetAllAuthors()
    {
        return _libraryAuthorsService.GetAllAuthors();
    }

    [HttpGet("authorsCount")]
    public int GetAllAuthorsCount()
    {
        return _libraryAuthorsService.GetAllAuthorsCount();
    }

    [HttpDelete("authors/{authorId}")]
    public IActionResult RemoveAuthor(int authorId)
    {
        _libraryAuthorsService.RemoveAuthor(authorId);

        return Ok("Author has been removed successfully");
    }

    [HttpGet("authorsBooksCount")]
    public IEnumerable<AuthorBooksCountResponse> GetAllAuthorsBooksCount(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
    {
        return _libraryAuthorsService.GetAllAuthorsWithBooksCount(search, authorId, sortBy, page, pageSize);
    }

    [HttpGet("books/{bookId}")]
    public BookResponse GetBookById(int bookId)
    {
        return _libraryBooksService.GetBookResponseById(bookId);
    }

    [HttpPost("addBook")]
    public IActionResult AddBook(CreateBookRequest request)
    {
        var title = request.Title;
        var author = request.Author;
        return Ok(_libraryBooksService.AddBook(title, author));
    }

    [HttpPost("updateBook")]
    public IActionResult UpdateBook(UpdateBookRequest request)
    {
        Console.WriteLine("request " + request.Author + " " + request.Title);
        return Ok(_libraryBooksService.UpdateBook(request));
    }

    [HttpDelete("books/{bookId}")]
    public IActionResult RemoveBook(int bookId)
    {
        return Ok(_libraryBooksService.RemoveBook(bookId));
    }

    [HttpPost("borrowBook")]
    public IActionResult BorrowBook(BorrowBookRequest request)
    {
        int bookId = request.Id;
        _libraryBooksService.Borrow(bookId);
        return Ok("Book is borrowd successfully");
    }

    [HttpPost("returnBook")]
    public IActionResult ReturnBook(ReturnBookRequest request)
    {
        int bookId = request.Id;
        _libraryBooksService.Return(bookId);
        return Ok("Book is returned successfully");
    }

}