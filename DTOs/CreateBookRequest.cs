namespace LibraryApi.Models;

public class CreateBookRequest
{
    public string? Title { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
}