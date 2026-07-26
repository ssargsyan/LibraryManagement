namespace LibraryMvc.Models;

public class AuthorsResponse
{
    public int Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public int BooksCount { get; set; }

}