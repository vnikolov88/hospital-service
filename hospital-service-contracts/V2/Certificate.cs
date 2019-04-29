namespace HospitalService.Contracts.V2
{
    public class Certificate
    {
        public string Name { get; set; }

        public string ExternalUrl { get; set; }

        public Picture[] Pictures { get; set; }
    }
}
