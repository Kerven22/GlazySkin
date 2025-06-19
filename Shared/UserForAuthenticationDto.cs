using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record UserForAuthenticationDto
    {
        [Required]
        public string? UserName { get; init; }
        [Required]
        public string? Password { get; init; }
    }
}
