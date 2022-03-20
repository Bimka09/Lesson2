namespace WebClient
{
    public class CustomerCreateRequest
    {
        public long id { get; init; }
        public string first_name { get; init; }
        public string last_name { get; init; }
        public string middle_name { get; init; }
        public string email { get; init; }
    }
}