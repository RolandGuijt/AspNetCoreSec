namespace AspNetSecurity_m3.Models
{
    public class ProposalModel
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string Speaker { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }
    }
}
