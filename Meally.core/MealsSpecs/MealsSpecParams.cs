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
    public Guid? CategoryId { get; set; }
    public Guid? RestaurantId { get; set; }

}
