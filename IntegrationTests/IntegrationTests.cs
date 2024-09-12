using Companies.API;
using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;


namespace IntegrationTests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient httpClient;
    private JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public IntegrationTests(WebApplicationFactory<Program> applicationFactory)
    {
        applicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:5000/api/");
        httpClient = applicationFactory.CreateClient();
    }

    [Fact]
    public async void IntegrationTest()
    {
        var response = await httpClient.GetAsync("demo");

        var result = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Hello from controller", result);
        Assert.Equal("text/plain", response.Content.Headers.ContentType!.MediaType);
    }

    [Fact]
    public async Task Index_ShouldRetrurnExpectedMediType()
    {
        var response = await httpClient.GetAsync("demo/dto");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        var dto = JsonSerializer.Deserialize<CompanyDto>(result, options);

        Assert.Equal("Working", dto?.Name);
        Assert.Equal("application/json", response.Content.Headers.ContentType!.MediaType);
    }

    [Fact]
    public async Task Index_ShouldRetrurnExpectedMediType2()
    {
        var dto = await httpClient.GetFromJsonAsync<CompanyDto>("demo/dto");
        Assert.Equal("Working", dto?.Name);
    }
}