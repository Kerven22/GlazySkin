using System.ComponentModel.DataAnnotations;

namespace Shared;

public record CategoryDto
{
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Name is Required field!")]
    public string Name { get; init; }
}