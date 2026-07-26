namespace LibraryAPI.Models;

public class BookEntity
{
    public int Id { get; private set; }
    public string? Title { get; set; } =string.Empty;
    // public string Author { get; private set; }

    public int? AuthorId { get; set; }

    public Author? Author { get; set; } = null!;

    public bool IsBorrowed { get; private set; }

    public BookEntity() { }

    // public BookEntity(string title, string author)
    // {

    //     Title = title;
    //     Author = author;
    //     IsBorrowed = false;
    // }

      public BookEntity(string title, int authorId)
    {

        Title = title;
        AuthorId=authorId;
        IsBorrowed = false;
    }

    public void Borrow()
    {
        IsBorrowed = true;
    }

    public void Return()
    {

        IsBorrowed = false;
    }
    public void Update(string title, int authorId)
    {
        Title = title;
        AuthorId = authorId;
    }

}