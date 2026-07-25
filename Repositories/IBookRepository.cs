using LibraryApi.Models;

namespace LibraryApi.Services;

public interface IBookRepository
{
    public List<BookResponse> GetAll(string? search, int? authorId, string? sortBy, int? page, int? pageSize);

    public int GetAllCount(string? search, int? authorId, string? sortBy, string? IsBorrowed);

    public BookEntity? GetById(int bookId);
    public BookEntity Add(BookEntity book);

    public BookEntity Remove(BookEntity book);

    public void Borrow(BookEntity book);

    public void Return(BookEntity book);

    public BookEntity Update(BookEntity book, string title, int authorId);

    public void Save();

}