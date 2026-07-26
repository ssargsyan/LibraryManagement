using System.ComponentModel.DataAnnotations;

namespace LibraryMvc.Models;

public class HomeViewModel
{

    public int TotalAuthors { get; set; }
    public int TotalBooks { get; set; }

    public int TotalBorrowedBooks { get; set; }

    public int TotalAvailableBooks { get; set; }

}