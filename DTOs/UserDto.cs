using System.ComponentModel.DataAnnotations;
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

                public GenderDto Gender { get; init; }

                public GenderDto SearchingGender { get; init; }

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

                public GenderDto Gender { get; init; }

                public GenderDto SearchingGender { get; init; }

                public IEnumerable<InterestDto> Interests { get; init; }

                public DateTime DateOfBirth { get; init; }
            }
        }

        public static class Request
        {
            public class List
            {
                public int? GenderId { get; init; }

                public int? SearchingGenderId { get; init; }

                public IEnumerable<int> InterestIds { get; init; }

                public int? AgeMoreThan { get; init; }

                public int? AgeLessThan { get; init; }
            }

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

                public int GenderId { get; init; }

                public int SearchingGenderId { get; init; }

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

                public int GenderId { get; init; }

                public int SearchingGenderId { get; init; }

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

                public int GenderId { get; init; }

                public int SearchingGenderId { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }
        }

        public class GenderDto
        {
            public int Id { get; init; }

            public string Name { get; init; }
        }

        public class InterestDto
        {
            public int Id { get; init; }

            public string Name { get; init; }
        }
    }
}
