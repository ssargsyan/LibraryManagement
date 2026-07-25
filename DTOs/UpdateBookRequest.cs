namespace LibraryApi.Models;

public class UpdateBookRequest
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
}