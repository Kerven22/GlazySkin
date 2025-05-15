using System.ComponentModel.DataAnnotations;

namespace Shared;

public record CategoryForCreationDto
{
    [Required(ErrorMessage = "Name is required fild!")]
    public string Name { get; init;  }

    public IEnumerable<ProductForCreationDto>? Products;
}