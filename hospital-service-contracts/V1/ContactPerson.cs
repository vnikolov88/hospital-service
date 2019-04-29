namespace HospitalService.Contracts.V1
{
    public class ContactPerson
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainAreaOfFocus { get; set; }
        public string PhonePrefix { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxPrefix { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
    }
}
