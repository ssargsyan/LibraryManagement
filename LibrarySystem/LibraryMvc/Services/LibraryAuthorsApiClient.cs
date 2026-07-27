using System.Text;
using System.Text.Json;
using LibraryMvc.Models;

namespace LibraryMvc.Services;

public class LibraryAuthorsApiClient
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions =
    new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LibraryAuthorsApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AuthorsResponse>> GetAuthorsAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/authorsBooksCount");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<AuthorsResponse>>
        (
            json,
          JsonOptions
        ) ?? [];
    }

     public async Task<int> GetAuthorsCountAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/library/authorsCount");

        response.EnsureSuccessStatusCode();

        var intValue = await response.Content.ReadAsStringAsync();

        return Convert.ToInt32(intValue);
    }

}