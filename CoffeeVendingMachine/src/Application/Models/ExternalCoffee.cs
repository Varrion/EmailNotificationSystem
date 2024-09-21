using CoffeeVendingMachine.Domain.Entities;
using Newtonsoft.Json;

namespace CoffeeVendingMachine.Application.Models;
public class ExternalCoffee : BaseCoffee
{
    [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? ExternalCoffeId { get; set; }

    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string? Description { get; set; }

    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string? Region { get; set; }

    [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
    public long? Weight { get; set; }

    [JsonProperty("flavor_profile", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> FlavorProfile { get; set; } = new List<string>();

    [JsonProperty("grind_option", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> GrindOption { get; set; } = new List<string>();

    [JsonProperty("roast_level", NullValueHandling = NullValueHandling.Ignore)]
    public long? RoastLevel { get; set; }

    [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
    public Uri? ImageUrl { get; set; }
}
