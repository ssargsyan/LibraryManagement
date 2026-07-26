using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryAPI.Services;

public class BookRepository : IBookRepository
{


    private readonly LibraryDbContext _context;


    public BookRepository(
        LibraryDbContext context)
    {
        _context = context;
    }
    private int _nextId = 1;


    public List<BookResponse> GetAll(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
    {
        // return _context.Books.Include(b => b.Author).ToList();

        var query = _context.Books.AsNoTracking().Include(b => b.Author).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(book => !string.IsNullOrWhiteSpace(book.Title) && book.Title.Contains(search));
        }
        if (authorId.HasValue)
        {
            query = query.Where(book => book.AuthorId == authorId);
        }
        query = sortBy?.ToLower() switch
        {
            "title" => query.OrderBy(b => b.Title),
            "author" => query.OrderBy(b => b.Author.Name),
            "id" => query.OrderBy(b => b.Id),
            _ => query.OrderBy(b => b.Id)
        };
        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip(Convert.ToInt32((page - 1) * pageSize)).Take(Convert.ToInt32(pageSize));
        }
        //  return query.ToList();
        return query.Select(book => new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author != null
        ? book.Author.Name
        : "Unknown",
            IsBorrowed = book.IsBorrowed
        }).ToList();
    }


    public int GetAllCount(string? search, int? authorId, string? sortBy, string? IsBorrowed)
    {
        var query = _context.Books.AsNoTracking().Include(b => b.Author).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(book => !string.IsNullOrWhiteSpace(book.Title) && book.Title.Contains(search));
        }
        if (authorId.HasValue)
        {
            query = query.Where(book => book.AuthorId == authorId);
        }
        if (!string.IsNullOrWhiteSpace(IsBorrowed))
        {
            query = query.Where(book => book.IsBorrowed == bool.Parse(IsBorrowed));
        }
        query = sortBy?.ToLower() switch
        {
            "title" => query.OrderBy(b => b.Title),
            "author" => query.OrderBy(b => b.Author.Name),
            "id" => query.OrderBy(b => b.Id),
            _ => query.OrderBy(b => b.Id)
        };

        //  return query.ToList();
        return query.Count();

    }

    public BookEntity? GetById(int bookId)
    {
        return _context.Books.Include(b => b.Author).FirstOrDefault(book => book.Id == bookId);
    }

    public BookEntity Add(BookEntity book)
    {
        _context.Books.Add(book);
        return book;
    }

    public BookEntity Remove(BookEntity book)
    {
        _context.Books.Remove(book);
        return book;
    }

    public void Borrow(BookEntity book)
    {
        book.Borrow();
    }

    public void Return(BookEntity book)
    {
        book.Return();
    }

    public BookEntity Update(BookEntity book, string title, int authorId)
    {
        book.Update(title, authorId);
        return book;
    }


    public void Save()
    {
        _context.SaveChanges();
    }
}