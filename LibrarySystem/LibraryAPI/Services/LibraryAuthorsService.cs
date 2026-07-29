using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Exceptions;

namespace LibraryAPI.Services;

public class LibraryAuthorsService : ILibraryAuthorsService
{

    private readonly List<Book> _books = new();

    private readonly IBookRepository _bookRepository;

    private readonly IAuthorRepository _authorRepository;

    private readonly LibraryDbContext _dbContext;

    public LibraryAuthorsService(
        IBookRepository bookRepository, IAuthorRepository authorRepository, LibraryDbContext dbContext)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _dbContext = dbContext;
    }

    public IEnumerable<Author> GetAllAuthors()
    {
        return _authorRepository.GetAll();
    }


    public int GetAllAuthorsCount()
    {
        return _authorRepository.GetAllCount();
    }

    public IEnumerable<AuthorBooksCountResponse> GetAllAuthorsWithBooksCount(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
    {
        return _authorRepository.GetAuthorsWithBooksCount(search, authorId, sortBy, page, pageSize);
    }

    public void RemoveAuthor(int authorId)
    {
        if (authorId <= 0)
        {
            throw new ValidationException("Author Id can't be negative");
        }
        var author = _authorRepository.GetById(authorId) ?? throw new NotFoundException("Author not found");

        _authorRepository.Remove(author);
        _dbContext.SaveChanges();
    }

}