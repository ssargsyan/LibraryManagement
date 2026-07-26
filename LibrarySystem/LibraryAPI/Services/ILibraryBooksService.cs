using LibraryAPI.Models;

namespace LibraryAPI.Services;

public interface ILibraryBooksService
{
    public IEnumerable<BookResponse> GetAllBooks(string? search, int? authorId, string? sortBy, int? page, int? pageSize);
    public int GetAllBooksCount(string? search, int? authorId, string? sortBy, string? IsBorrowed);
    public BookResponse GetBookResponseById(int bookId);

    // public Book GetBookById(int bookId);

    // public BookEntity GetBookById(int bookId);
    public BookResponse AddBook(string? title, string? author);

    public BookResponse RemoveBook(int bookId);

    public void Borrow(int bookId);

    public void Return(int bookId);

    public BookResponse UpdateBook(UpdateBookRequest request);

}