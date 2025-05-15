using System.ComponentModel.DataAnnotations;

namespace Shared;

public record ProductDto
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    [Required(ErrorMessage = "Name is required field!")]
    public string Name { get; init; }
    [Required(ErrorMessage = "Cost is required field!")]
    public decimal Cost { get; init; }
    [Required(ErrorMessage = "Review is required field!")]
    public string Review { get; init; }
    
}