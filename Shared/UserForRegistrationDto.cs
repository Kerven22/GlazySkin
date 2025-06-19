using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record UserForRegistrationDto
    {
        public string? UserName { get; init; }
        public string? LastName { get; init; }
        [Required]
        public string? Login { get; init; }
        [Required]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        public ICollection<string>? Roles { get; init; }
    }
}
