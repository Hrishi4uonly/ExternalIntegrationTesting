using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CatalogueService.Services.Models;
using CatalogueService.Tests.WebApplicationFixture;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace CatalogueService.Tests.IntegrationTests
{
    [Collection(nameof(WebApplicationTestCollection))]
    public class ProductsCatalogueTests
    {
       // private readonly WebApplicationTest _factory;
        private readonly WebApplicationTest _factory;

        public ProductsCatalogueTests(ITestOutputHelper testOutputHelper, WebApplicationTest factory)
        {
             _factory = factory;
                _factory.TestOutputHelper = testOutputHelper;
           
           
        }

        [Fact]
        private async Task ShouldReturnExpectedResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var mediaType = MediaTypeHeaderValue.Parse("text/plain; charset=iso-8859-5");

            var getIphone= _factory.InventoryServiceMock.Response_to_GetInventory("1", 2);
            var getMacbook = _factory.InventoryServiceMock.Response_to_GetInventory("2", 4);
            var getapplewatch= _factory.InventoryServiceMock.Response_to_GetInventory("3", 6);

            // Act
            var response = await client.GetAsync("http://localhost:5010/v1/catalogue/products");
            //var mediaType = MediaTypeHeaderValue.Parse("text/plain; charset=iso-8859-5");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultCatalogue = await response.Content.ReadFromJsonAsync<IEnumerable<CatalogueEntry>>();

            _factory.TestOutputHelper.WriteLine(JsonSerializer.Serialize(resultCatalogue));

            var expectedCatalogue = new CatalogueEntry[]
            {
                new()
                {
                    ProductId = "1",
                    Description = "Iphone15",
                    Quantity = 2
                },
            new()
            {
                    ProductId = "2",
                    Description = "MACBOOK",
                    Quantity = 4
            },
            new()
            {
                    ProductId = "3",
                    Description = "Apple Watch",
                    Quantity = 6
            }
            };

            resultCatalogue.Should().BeEquivalentTo(expectedCatalogue);
            getIphone.ShouldBeCompleted();
            getMacbook.ShouldBeCompleted();
            getapplewatch.ShouldBeCompleted();
        }
    }
}