namespace Shared;

public record ProductDto
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
    public decimal Cost { get; init; }
    public string Review { get; init; }
    
}