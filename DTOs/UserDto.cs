using Common.Enums;

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

                public string Gender { get; init; }

                public string SearchingGender { get; init; }

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
                public string UserName { get; init; }

                public string Password { get; init; }

                public string FirstName { get; init; }

                public string LastName { get; init; }

                public GenderEnum Gender { get; init; }

                public GenderEnum SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }

            public class Update
            {
                public int Id { get; init; }

                public string UserName { get; init; }

                public string Password { get; init; }

                public string FirstName { get; init; }

                public string LastName { get; init; }

                public string Gender { get; init; }

                public string SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }

            public class UpdateMe
            {
                public string UserName { get; init; }

                public string FirstName { get; init; }

                public string LastName { get; init; }

                public string Gender { get; init; }

                public string SearchingGender { get; init; }

                public string City { get; init; }

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }
            }
        }
    }
}
