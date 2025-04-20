using Localize.Company.Api.Validations;
using System.ComponentModel.DataAnnotations;

namespace Localize.Company.Api.Requests
{
    public class SignUpRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [ValidEmail(ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
