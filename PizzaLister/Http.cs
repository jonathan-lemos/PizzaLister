using System.Text.Json;

namespace PizzaLister;

public static class Http
{
    public static async Task<T> Get<T>(string url)
    {
        // the problem says to use WebClient/HttpWebRequest, but Rider says they're both deprecated atm
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var responseText = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               }) ??
               throw new InvalidDataException($"The response could not be deserialized into a {typeof(T).Name}. Response: {response}");
    }
}