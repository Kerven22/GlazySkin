namespace Shared.RequestFeatures;

public class ProductParameters:RequestParameteres
{
    public uint MinCost { get; set; } = 0;
    public uint MaxCost { get; set; } = int.MaxValue;

    public bool ValidCostRange => MinCost < MaxCost; 
}