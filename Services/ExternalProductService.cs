using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ComputerShop.Models;

namespace ComputerShop.Services
{
    public class ExternalProductService : IExternalProductService
    {
        private readonly HttpClient _httpClient;

        public ExternalProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ExternalProduct>> GetExternalProductsAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ExternalProduct>>("https://api.escuelajs.co/api/v1/products");
            return products;
        }
    }
}
