using Ardalis.HttpClientTestExtensions;

namespace TodoList.Api.FunctionTests.ApiEndpoints;

[Collection("Sequential")]
public class ToDoItems : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
    private readonly HttpClient _client;

    public ToDoItems(CustomWebApplicationFactory<WebMarker> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsOneProject()
    {
        var result = await _client.GetAndDeserializeAsync<IEnumerable<ToDoItems>>("/api/todoitems");

        Assert.Equal(4, result.Count());
    }
}
