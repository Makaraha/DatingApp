using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class AuthDto
    {
        public class AuthByEmail
        {
            [EmailAddress]
            public string Email { get; set; }

            public string Password { get; set; }
        }
    }
}
