using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using WebApp.ApiControllers.identity;
using Xunit.Abstractions;

namespace Tests.Integration.api.identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public IdentityIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
            
        });
    }

    [Fact(DisplayName = "Post - register new user")]
    public async Task RegisterNewUser()
    {
        var URL = "/api/v1/identity/account/register";

        var registerData = new
        {
            Email = "test@test.io",
            Password = "Qqwerty-1",
            Firstname = "First name",
            Lastname = "Last name",
        };

        var data = JsonContent.Create(registerData);

        var response = await _client.PostAsync(URL, data);

        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        
        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<JWTResponse>(responseContent);
        
        Assert.NotNull(jwtResponse?.JWT);
    }
    
    [Fact(DisplayName = "Post - login new user")]
    public async Task LoginWithNewUser()
    {
        var URL = "/api/v1/identity/account/login";

        var registerData = new
        {
            Email = "test@test.com",
            Password = "Qqwerty-1",
        };

        var data = JsonContent.Create(registerData);

        var response = await _client.PostAsync(URL, data);

        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent, options);
        
        Assert.NotNull(jwtResponse?.JWT);
    }
    [Fact(DisplayName = "Post - logout new user")]
    public async Task LogOutWithNewUser()
    {
        var URL = "/api/v1/identity/account/login";
        var LogoutUrl = "/api/v1/identity/account/logout";

        var registerData = new
        {
            Email = "test@test.com",
            Password = "Qqwerty-1",
        };

        var data = JsonContent.Create(registerData);

        var response = await _client.PostAsync(URL, data);

        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent, options);
        
        Assert.NotNull(jwtResponse?.JWT);
        
        var logoutData = new
        {
            RefreshToken = jwtResponse.RefreshToken,
        };
        
        var dataLogout = JsonContent.Create(logoutData);
        
        var responseLogout = await _client.PostAsync(LogoutUrl, dataLogout);
        
        Assert.True(responseLogout.IsSuccessStatusCode);
        
        var logoutContent = await responseLogout.Content.ReadAsStringAsync();
        
        Assert.Contains(logoutContent, "TokenDeleteCount");
        
    }
}