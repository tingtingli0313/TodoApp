using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System.Net;

namespace TodoList.Api.FunctionTests;
public class TestWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly Mock<IHttpClientFactory> _mockFactory;

    public bool BypassAuthorization { get; set; }

    public TestWebApplicationFactory()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var client = new HttpClient(_mockHttpMessageHandler.Object);
        client.BaseAddress = new Uri("https://test.com/");

        _mockFactory = new Mock<IHttpClientFactory>();
        _mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(sp => sp.AddScoped(_ => _mockFactory.Object));
    }

    public void MockHttpResponse(HttpStatusCode statusCode, string requestUrl, string content)
    {
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(msg => msg.RequestUri!.ToString().EndsWith(requestUrl)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content),
            });
    }

    public void MockHttpResponse<T>(HttpStatusCode statusCode, string requestUrl, T content)
    {
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(msg => msg.RequestUri!.ToString().ToLowerInvariant().EndsWith(requestUrl.ToLowerInvariant())),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(JsonConvert.SerializeObject(content)),
            });
    }

    public void ClearInvocations()
    {
        _mockHttpMessageHandler.Invocations.Clear();
    }

    public void VerifyHttpResponse(string url, Times times)
    {
        _mockHttpMessageHandler
            .Protected()
            .Verify("SendAsync", times,
                ItExpr.Is<HttpRequestMessage>(msg => msg.RequestUri!.ToString().ToLowerInvariant().EndsWith(url.ToLowerInvariant())),
                ItExpr.IsAny<CancellationToken>());
    }

    public ILogger<T> GetTestDebuggingLogger<T>()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(x =>
            {
                x.AddDebug();
            })
            .BuildServiceProvider();

        return serviceProvider.GetService<ILogger<T>>();
    }
}
