using System.ComponentModel.DataAnnotations;

namespace LibraryMvc.Models;

public class BookFormViewModel
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    [StringLength(100)]
    public string? Author { get; set; }
}