namespace LibraryApi.Models;

public class Author
{

    public int Id { get; private set; }

    public string? Name { get; set; } = string.Empty;

    public Author() { }

    public Author(string name)
    {
        Name = name;
    }

    public ICollection<BookEntity> Books { get; private set;} = new List<BookEntity>();
}