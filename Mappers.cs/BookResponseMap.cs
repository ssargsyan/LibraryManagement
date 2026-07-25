namespace LibraryApi.Models;

public class BookResponseMap
{

    public BookResponse ToResponseBook(Book book)
    {
        return new BookResponse { Id = book.Id, Title = book.Title, Author = book.Author, IsBorrowed = book.IsBorrowed };
    }

    public BookResponse ToResponseBook(BookEntity book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author != null
        ? book.Author.Name
        : "Unknown",
            IsBorrowed = book.IsBorrowed
        };
    }
    public IEnumerable<BookResponse> ToResponseBooks(List<Book> books)
    {
        return books.Select(book => ToResponseBook(book));
    }

    public IEnumerable<BookResponse> ToResponseBooks(List<BookEntity> books)
    {
        return books.Select(book => ToResponseBook(book));
    }
}