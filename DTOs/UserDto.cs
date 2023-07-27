using System.ComponentModel.DataAnnotations;
using Common.Enums;
using Common.Interfaces;

namespace DTOs
{
    public static class UserDto
    {
        public static class Response
        {
            public record ById
            {
                public int Id { get; init; }

                public string UserName { get; init; }

                public string FirstName { get; init; }

                public string LastName { get; init; }

                public string Email { get; init; }

                public string PhoneNumber { get; init; }

                public GenderEnum Gender { get; init; }

                public GenderEnum SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }

                public DateTime CreatedDate { get; init; }

                public DateTime LastModifiedDate { get; init; }

                public bool IsDeleted { get; init; }
            }

            public record List
            {
                public int Id { get; init; }

                public string UserName { get; init; }

                public string About { get; init; }

                public string City { get; init; }
            }
        }

        public static class Request
        {
            public class Add
            {
                [Required, MaxLength(32)]
                public string UserName { get; init; }

                [Required]
                public string Password { get; init; }

                [MaxLength(32)]
                public string FirstName { get; init; }

                [MaxLength(32)]
                public string LastName { get; init; }

                [Required, EmailAddress]
                public string Email { get; init; }

                [Required, Phone]
                public string PhoneNumber { get; init; }

                public GenderEnum Gender { get; init; }

                public GenderEnum SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }

            public class Update : IIdHas<int>
            {
                [Required]
                public int Id { get; init; }

                [Required, MaxLength(32)]
                public string UserName { get; init; }

                [MaxLength(32)]
                public string FirstName { get; init; }

                [MaxLength(32)]
                public string LastName { get; init; }

                public GenderEnum Gender { get; init; }

                public GenderEnum SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }

            public class UpdateMe
            {
                [Required, MaxLength(32)]
                public string UserName { get; init; }

                [MaxLength(32)]
                public string FirstName { get; init; }

                [MaxLength(32)]
                public string LastName { get; init; }

                public GenderEnum Gender { get; init; }

                public GenderEnum SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }
        }
    }
}
