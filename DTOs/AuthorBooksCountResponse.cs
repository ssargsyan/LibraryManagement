namespace LibraryApi.Models;

public class AuthorBooksCountResponse
{
    // public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int BooksCount { get; set; }
}