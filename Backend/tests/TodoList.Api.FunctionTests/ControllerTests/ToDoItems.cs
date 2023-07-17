using Ardalis.HttpClientTestExtensions;
using Azure;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoList.Api.ApiModels;

namespace TodoList.Api.FunctionTests.ApiEndpoints;

[Collection("Sequential")]
public class ToDoItems : IClassFixture<TestWebApplicationFactory<Startup>>
{
    private readonly HttpClient _httpClient;
    private readonly TestWebApplicationFactory<Startup> factory;
    private readonly JsonSerializerOptions EnumsFromStrings = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };


    public ToDoItems(TestWebApplicationFactory<Startup> factory)
    {
        this.factory = factory;
        _httpClient = factory.CreateClient();
    }


    [Fact]
    public async Task ReturnsAllDefaultTodoItems()
    {
        var result = await _httpClient.GetAsync("/api/todoitems");
        var content = await result.Content.ReadAsStringAsync();
        var allItems = JsonSerializer.Deserialize<List<TodoItemDTO>>(content, EnumsFromStrings);

        Assert.True(result.IsSuccessStatusCode);
        Assert.True(allItems.Count() == 3);
    }
}
