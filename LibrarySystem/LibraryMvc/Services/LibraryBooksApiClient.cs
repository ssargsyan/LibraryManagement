using System.Text;
using System.Text.Json;
using LibraryMvc.Models;

namespace LibraryMvc.Services;

public class LibraryBooksApiClient
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions =
    new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LibraryBooksApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BookResponse>> GetBooksAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/books");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<BookResponse>>
        (
            json,
          JsonOptions
        ) ?? [];
    }

    public async Task<int> GetBooksCountAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/booksCount");

        response.EnsureSuccessStatusCode();

        var intValue = await response.Content.ReadAsStringAsync();

        return Convert.ToInt32(intValue);
    }

    public async Task<int> GetBorrowedBooksCountAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/booksCount?isBorrowed=true");

        response.EnsureSuccessStatusCode();

        var intValue = await response.Content.ReadAsStringAsync();

        return Convert.ToInt32(intValue);
    }

    public async Task<int> GetAvailableBooksCountAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/booksCount?isBorrowed=false");

        response.EnsureSuccessStatusCode();

        var intValue = await response.Content.ReadAsStringAsync();

        return Convert.ToInt32(intValue);
    }

    public async Task<BookResponse> GetBookAsync(int bookId)
    {
        var response = await _httpClient.GetAsync(
            $"api/library/books/{bookId}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<BookResponse>
        (
            json,
            JsonOptions
        );
        return book ?? throw new InvalidOperationException(
    "Failed to deserialize response");
    }

    public async Task RemoveBookAsync(int bookId)
    {

        var response = await _httpClient.DeleteAsync(
            $"api/library/books/{bookId}");

        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeBorrowStatusAsync(int bookId, bool isBorrowed)
    {
        var request = new
        {
            Id = bookId
        };

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");
        var requestUrl = "";
        if (isBorrowed)
        {
            requestUrl = "api/library/borrowBook";
        }
        else
        {
            requestUrl = "api/library/returnBook";
        }
        var response = await _httpClient.PostAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<BookResponse> CreateBookAsync(CreateBookRequest request)
    {

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("api/library/addBook", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<BookResponse>
        (
            responseJson,
            JsonOptions
        );
        return book ?? throw new InvalidOperationException(
    "Failed to deserialize response");
    }

    public async Task<BookResponse> UpdateBookAsync(UpdateBookRequest request)
    {

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("api/library/updateBook", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<BookResponse>
           (
               responseJson,
               JsonOptions
           );
        return book ?? throw new InvalidOperationException(
    "Failed to deserialize response");
    }
}