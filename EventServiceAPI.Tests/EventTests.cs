using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class EventTests
{
    private readonly HttpClient _Client;

    public EventTests()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostEvent_Valid_ReturnsAccepted()
    {
        var response = await _client.PostAsJsonAsync("/events", new
        {
            type = "tests",
            source = "test"
        });

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }

    [Fact]
    public async Task PostEvent_Invalid_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/events", new
        {
            type = "",
            source = ""
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}