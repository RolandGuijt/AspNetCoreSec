using System.Collections.Generic;
using System.Linq;
using AspNetSecurity_m4_Shared.Models;

namespace AspNetSecurity_m4_Api.Repositories
{
    public class AttendeeRepo
    {
        private readonly List<AttendeeModel> attendees = new List<AttendeeModel>();

        public AttendeeRepo()
        {
            attendees.Add(new AttendeeModel {Id = 1, ConferenceId = 1, Name = "Anders Heijlsberg"});
            attendees.Add(new AttendeeModel { Id = 2, ConferenceId = 1, Name = "Rob Eisenberg" });
            attendees.Add(new AttendeeModel { Id = 3, ConferenceId = 2, Name = "John Mashmellow" });
        }

        public AttendeeModel GetById(int attendeeId)
        {
            return attendees.First(a => a.Id == attendeeId);
        }

        public AttendeeModel Add(AttendeeModel attendee)
        {
            attendee.Id = attendees.Max(a => a.Id) + 1;
            attendees.Add(attendee);
            return attendee;
        }

        public int GetAttendeesTotal(int conferenceId)
        {
            return attendees.Count(a => a.ConferenceId == conferenceId);
        }
    }
}
