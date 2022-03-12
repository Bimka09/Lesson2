using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Client
    {
        public long id { get; init; }
        
        [Required]
        public string first_name { get; init; }

        [Required]
        public string last_name { get; init; }
        [Required]
        public string middle_name { get; init; }
        [Required]
        public string email { get; init; }
    }
}