using Companies.API;
using Microsoft.AspNetCore.Mvc.Testing;


namespace IntegrationTests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient httpClient;

    public IntegrationTests(WebApplicationFactory<Program> applicationFactory)
    {
        httpClient = applicationFactory.CreateDefaultClient();
    }

    [Fact]
    public async void IntegrationTest()
    {
        var response = await httpClient.GetAsync("api/demo");

        var result = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Hello from controller", result);
    }
}