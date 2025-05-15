using System.ComponentModel.DataAnnotations;

namespace Shared;

public record ProductForCreationDto
{
    [Required(ErrorMessage = "Name is required field")]
    public string Name { get; init;}
    
    [Required(ErrorMessage = "Cost is required field!")]
    public decimal Cost { get; init; }

    public string Review { get; init;}
    
    [Required(ErrorMessage = "Quantity is required field!")]
    public int Quantity { get; init; }
    
}; 