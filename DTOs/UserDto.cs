﻿namespace DTOs
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

                public string About { get; init; }

                public DateTime DateOfBirth { get; init; }

                public DateTime CreatedDate { get; init; }

                public DateTime LastModifiedDate { get; init; }

                public bool IsDeleted { get; init; }
            }
        }

        public static class Request
        {

        }
    }
}
