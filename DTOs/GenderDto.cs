using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public static class GenderDto
    {
        public static class Request
        {
            public class Add
            {
                [MaxLength(64), Required]
                public string Name { get; set; }
            }

            public class Update
            {
                [Required]
                public int Id { get; set; }

                [MaxLength(64), Required]
                public string Name { get; set; }
            }

            public class AddTranslation
            {
                [Required]
                public int GenderId { get; set; }

                [MaxLength(16), Required]
                public string CultureName { get; set; }

                [MaxLength(256), Required]
                public string LocalizedName { get; set; }
            }
        }

        public static class Response
        {
            public class List
            {
                public int Id { get; set; }

                public string Name { get; set; }
            }

            public class ById
            {
                public int Id { get; set; }

                public string Name { get; set; }
            }
        }
    }
}
