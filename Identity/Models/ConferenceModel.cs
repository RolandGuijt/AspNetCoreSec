using System;

namespace AspNetSecurity_m3.Models
{
    public class ConferenceModel
    {
        public ConferenceModel()
        {
            Start = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public string Location { get; set; }
    }
}
