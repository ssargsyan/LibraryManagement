using System.ComponentModel.DataAnnotations;

namespace LibraryMvc.Models;

public class CreateBookRequest
{
    public string? Title { get; set; }

    public string? Author { get; set; }
}