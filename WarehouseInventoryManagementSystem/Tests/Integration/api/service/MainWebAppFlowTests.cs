using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Xunit.Abstractions;
using WarehouseInventory = Domain.App.WarehouseInventory;

namespace Tests.Integration.api.service;

public class MainWebAppFlowTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    private static string JWT =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImEwY2VkNWYxLTNlYjgtNGI1NC04OTQzLTdlMGVjNjkyZTZmMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJybWF6YW50c2V2QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InJtYXphbnRzZXZAZ21haWwuY29tIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiJMRjdEU0lXSEEzNkQzT09NVUJSWFk2TjdMQk5JRkVPQiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IlJvbWFuIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6Ik1hemFudHNldiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjg2MzgyOTY3LCJpc3MiOiJyYW1hem9uLmNvbSIsImF1ZCI6InJhbWF6b24uY29tIn0.pZ0n7r05aTIhQPDFNSjAtWeT03XOtfhPjDlL0qYRIng";

    public MainWebAppFlowTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "Post - create new warehouse")]
    public async Task CreateNewWarehouse()
    {
        var URL = "/api/v1/warehouses";
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);
        var registerData = new
        {
            Name = "Das Companies, Inc",
            Address = "Tulsa, Oklahoma",
        };

        var data = JsonContent.Create(registerData);
        var response = await _client.PostAsync(URL, data);
         
        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<Warehouse>(responseContent);
        
        Assert.NotNull(jwtResponse?.Id);
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
    
    [Fact(DisplayName = "Post - Create New Item")]
    public async Task CreateNewItem()
    {
        var URL = "/api/v1/items";
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);
        var itemData = new
        {
            Name = "Kolbasa",
            Description = "Tulsa, Oklahoma",
            UnitId = "09c81bae-6015-4a3b-926e-b16b38f56c6c"
        };

        var data = JsonContent.Create(itemData);
        var response = await _client.PostAsync(URL, data);
         
        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var item = System.Text.Json.JsonSerializer.Deserialize<Item>(responseContent);
        
        Assert.NotNull(item?.Id);
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
    
    [Fact(DisplayName = "Post - Create New Warehouse Inventory")]
    public async Task CreateNewWarehouseInventory()
    {
        var URL = "/api/v1/warehouseinventories";
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);
        var inventoryData = new
        {
            Quantity= "33333",
            ItemId = "1baa7c8d-56cc-45dc-aa2a-75cbb4e80013",
            WarehouseId= "21467176-122d-4821-8cc9-6aad6f1ce705"
        };

        var data = JsonContent.Create(inventoryData);
        var response = await _client.PostAsync(URL, data);
         
        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var inventory = System.Text.Json.JsonSerializer.Deserialize<WarehouseInventory>(responseContent);
        
        Assert.NotNull(inventory?.Id);
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
}