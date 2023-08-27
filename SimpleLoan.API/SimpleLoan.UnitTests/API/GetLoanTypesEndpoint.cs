using System.Net;
using FluentAssertions;
using System.Text.Json;

namespace SimpleLoan.UnitTests.API;

public class GetLoanTypesEndpoint
{
    private readonly WebApplicationFactory<Program> _factory;

    public GetLoanTypesEndpoint()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Fact]
    public async Task GetLoanTypes_ShouldReturnAvailableTypes()
    {
        HttpClient httpClient = _factory.CreateClient();

        var response = await httpClient.GetAsync("/loan/types");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        string json = await response.Content.ReadAsStringAsync();
        json.Should().NotBeNullOrEmpty();

        string[] result = JsonSerializer.Deserialize<string[]>(json);
        result.Length.Should().BePositive();
    }
}