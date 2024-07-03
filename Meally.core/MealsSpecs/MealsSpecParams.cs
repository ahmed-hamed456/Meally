namespace Meally.core.RestaurantsSpecs;

public class MealsSpecParams
{
    private string? search;

    public string? Search
    {
        get => search;
        set => search = value?.ToLower();
    }

    public string? Sort { get; set; }
    public int? CategoryId { get; set; }
    public int? RestaurantId { get; set; }

}
