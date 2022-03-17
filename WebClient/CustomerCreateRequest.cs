namespace WebClient
{
    public class CustomerCreateRequest
    {
        public CustomerCreateRequest()
        {

        }
        public CustomerCreateRequest(long Id, string firstName, string middleName, 
                                        string lastName, string mail)
        {
            id = Id;
            first_name = firstName;
            middle_name = middleName;
            last_name = lastName;
            email = mail;
        }

        public long id { get; init; }
        public string first_name { get; init; }
        public string last_name { get; init; }
        public string middle_name { get; init; }
        public string email { get; init; }
    }
}