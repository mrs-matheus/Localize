using Localize.Company.Api.Validations;
using System.ComponentModel.DataAnnotations;

namespace Localize.Company.Api.Requests
{
    public class SignInRequest
    {
        [ValidEmail]
        public string Email { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
