using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryAPI.Services;

public class AuthorRepository : IAuthorRepository
{

    private readonly LibraryDbContext _context;


    public AuthorRepository(
        LibraryDbContext context)
    {
        _context = context;
    }
    public List<Author> GetAll()
    {
        return _context.Authors.AsNoTracking().ToList();
    }


    public int GetAllCount()
    {
        return _context.Authors.Count();
    }

    public Author? GetAuthorByName(string name)
    {
        return _context.Authors.FirstOrDefault(author => author.Name == name);
    }

    public Author? GetById(int authorId)
    {
        return _context.Authors.FirstOrDefault(author => author.Id == authorId);
    }
    public Author Add(Author author)
    {
        _context.Authors.Add(author);
        return author;
    }

    public Author Remove(Author author)
    {
        _context.Authors.Remove(author);
        return author;
    }

    public List<AuthorBooksCountResponse> GetAuthorsWithBooksCount(string? search, int? authorId, string? sortBy, int? page, int? pageSize)
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
        return query.GroupBy(b => b.Author.Name)
    .Select(group => new AuthorBooksCountResponse
    {
        Name = group.Key,
        BooksCount = group.Count()
    }).ToList();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}