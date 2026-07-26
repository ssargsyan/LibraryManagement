using System.ComponentModel.DataAnnotations;

namespace LibraryMvc.Models;

public class UpdateBookRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }
}