namespace LibraryApi.Models;

public class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsBorrowed { get; private set; }

    public Book(int id, string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title can't be empty");
        }
        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author can't be empty");
        }
        Id = id;
        Title = title;
        Author = author;
        IsBorrowed = false;
    }

    public void Borrow()
    {
        if (IsBorrowed)
        {
            throw new InvalidOperationException("Book already has been borrowed");
        }
        IsBorrowed = true;
    }

    public void Return()
    {
        if (!IsBorrowed)
        {
            throw new InvalidOperationException("Book already has been returned");
        }
        IsBorrowed = false;
    }
    public void Update(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title can't be empty");
        }
        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author can't be empty");
        }
        Title = title;
        Author = author;
    }

}