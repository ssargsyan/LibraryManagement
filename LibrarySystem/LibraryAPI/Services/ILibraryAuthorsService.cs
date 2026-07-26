using LibraryAPI.Models;

namespace LibraryAPI.Services;

public interface ILibraryAuthorsService
{
    public IEnumerable<Author> GetAllAuthors();

    public int GetAllAuthorsCount();

    public void RemoveAuthor(int authorId);

    public IEnumerable<AuthorBooksCountResponse> GetAllAuthorsWithBooksCount(string? search, int? authorId, string? sortBy, int? page, int? pageSize);

}