using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryAPI.Services;

public class LibraryAuthorsService : ILibraryAuthorsService
{

    private readonly List<Book> _books = new();

    private readonly IBookRepository _bookRepository;

    private readonly IAuthorRepository _authorRepository;
    public LibraryAuthorsService(
        IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
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
            throw new InvalidOperationException("Author Id can't be negative");
        }
        var author = _authorRepository.GetById(authorId) ?? throw new InvalidOperationException("Author not found");

        _authorRepository.Remove(author);
        _authorRepository.Save();
    }

}