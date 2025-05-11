namespace Shared;

public record CategoryForCreationDto(string name, IEnumerable<ProductForCreationDto>? products); 