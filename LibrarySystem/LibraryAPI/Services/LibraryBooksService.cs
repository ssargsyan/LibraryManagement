using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Exceptions;
namespace LibraryAPI.Services;

public class LibraryBooksService : ILibraryBooksService
{

    private readonly List<Book> _books = new();

    private readonly IBookRepository _bookRepository;

    private readonly IAuthorRepository _authorRepository;

    private readonly LibraryDbContext _dbContext;

    public LibraryBooksService(
        IBookRepository bookRepository, IAuthorRepository authorRepository, LibraryDbContext dbContext)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _dbContext = dbContext;
    }
    private int _nextId = 1;

    public BookResponseMap _bookMapper = new BookResponseMap();

    public IEnumerable<BookResponse> GetAllBooks(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
    {
        // return _bookMapper.ToResponseBooks(_books);
        // return _bookMapper.ToResponseBooks(_bookRepository.GetAll(search, authorId, sortBy, page, pageSize));
        //after implementing Projection
        return _bookRepository.GetAll(search, authorId, sortBy, page, pageSize);

    }

    public int GetAllBooksCount(string? search, int? authorId, string? sortBy, string? IsBorrowed)
    {
        return _bookRepository.GetAllCount(search, authorId, sortBy, IsBorrowed);
    }

    // public BookEntity GetBookById(int bookId)
    // {

    //     //return _books.FirstOrDefault(book => book.Id == bookId) ?? throw new InvalidOperationException("Book not found"); ;

    //     return _bookRepository.GetById(bookId) ?? throw new InvalidOperationException("Book not found");

    // }

    public BookResponse GetBookResponseById(int bookId)
    {
        // var book = _books.FirstOrDefault(book => book.Id == bookId) ?? throw new InvalidOperationException("Book not found");
        var book = _bookRepository.GetById(bookId) ?? throw new NotFoundException(
            $"Book {bookId} not found");
        return _bookMapper.ToResponseBook(book);

        //after implementing Projection
        // return book;
    }

    public BookResponse AddBook(string? title, string? author)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            throw new ValidationException("Added book must have title and author");
        }
        // var book = new Book(_nextId++, title, author);

        // _books.Add(book);
        // return _bookMapper.ToResponseBook(book);

        // using db context

        var foundAuthor = _authorRepository.GetAuthorByName(author) ?? null;
        if (foundAuthor == null)
        {
            var addedAuthor = _authorRepository.Add(new Author(author));
            foundAuthor = addedAuthor;
        }

        var book = new BookEntity(title, foundAuthor);
        _bookRepository.Add(book);
        _dbContext.SaveChanges();
        return _bookMapper.ToResponseBook(book);
    }

    public BookResponse RemoveBook(int bookId)
    {
        if (bookId <= 0)
        {
            throw new ValidationException("Book Id can't be negative");
        }
        var book = _bookRepository.GetById(bookId) ?? throw new NotFoundException($"Book {bookId} not found");
        //  _books.Remove(book);
        _bookRepository.Remove(book);
        _dbContext.SaveChanges();
        return _bookMapper.ToResponseBook(book);
    }

    public void Borrow(int bookId)
    {
        if (bookId <= 0)
        {
            throw new ValidationException("Book Id can't be negative");
        }
        var book = _bookRepository.GetById(bookId) ?? throw new NotFoundException($"Book {bookId} not found");
        Console.WriteLine($"================Borrow {book.Title}===========");
        _bookRepository.Borrow(book);
        _dbContext.SaveChanges();
    }

    public void Return(int bookId)
    {
        if (bookId <= 0)
        {
            throw new ValidationException("Book Id can't be negative");
        }
        var book = _bookRepository.GetById(bookId) ?? throw new NotFoundException($"Book {bookId} not found");
        _bookRepository.Return(book);
        _dbContext.SaveChanges();
    }


    // public BookResponse UpdateBook(UpdateBookRequest request)
    // {
    //     var id = request.Id;
    //     var titile = request.Title;
    //     var author = request.Author;
    //     var book = _bookRepository.GetById(id) ?? throw new InvalidOperationException("Book not found");
    //     _bookRepository.Update(book,titile, author);
    //     _bookRepository.Save();
    //     return _bookMapper.ToResponseBook(book);
    // }

    public BookResponse UpdateBook(UpdateBookRequest request)
    {

        var id = request.Id;
        var titile = request.Title;
        var authorName = request.Author;
        var book = _bookRepository.GetById(id) ?? throw new NotFoundException($"Book {id} not found");

        Console.WriteLine($"{authorName} ========= ");
        var author = _authorRepository.GetAuthorByName(authorName);
        if (author == null)
        {
            var createdAuthor = _authorRepository.Add(new Author(authorName));
            author = createdAuthor;
        }

        _bookRepository.Update(book, titile, author);
        _dbContext.SaveChanges();
        return _bookMapper.ToResponseBook(book);

    }
}