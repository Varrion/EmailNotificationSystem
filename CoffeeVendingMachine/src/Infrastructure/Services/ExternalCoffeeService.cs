using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Models;
using Newtonsoft.Json;

namespace CoffeeVendingMachine.Infrastructure.Services;
public class ExternalCoffeeService : IExternalCoffeeService
{
    private readonly HttpClient _httpClient;
    private const string ExternalCoffeeApi = "https://fake-coffee-api.vercel.app/api";

    public ExternalCoffeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExternalCoffee?> GetExternalCoffeeByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"{ExternalCoffeeApi}/{id}");
            ExternalCoffee? result = JsonConvert.DeserializeObject<ExternalCoffee>(response);

            return result;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<IEnumerable<ExternalCoffee>> GetExternalCoffeesAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(ExternalCoffeeApi);

            IEnumerable<ExternalCoffee>? result = JsonConvert.DeserializeObject<IEnumerable<ExternalCoffee>>(response);

            if (result == null || result.Count() == 0)
            {
                return [];
            }


            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
