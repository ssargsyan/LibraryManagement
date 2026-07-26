using LibraryAPI.Models;

namespace LibraryAPI.Services;

public interface IAuthorRepository
{
    public List<Author> GetAll();

    public int GetAllCount();
    public Author Add(Author author);

    public Author Remove(Author author);
    public Author? GetAuthorByName(string name);

      public Author? GetById(int authorId);

    public List<AuthorBooksCountResponse> GetAuthorsWithBooksCount(string? search, int? authorId, string? sortBy, int? page, int? pageSize);

    public void Save();

}