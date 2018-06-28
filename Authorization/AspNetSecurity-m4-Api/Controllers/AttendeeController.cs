using AspNetSecurity_m4_Api.Repositories;
using AspNetSecurity_m4_Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m4_Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AttendeeController : Controller
    {
        private readonly AttendeeRepo repo;

        public AttendeeController(AttendeeRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public AttendeeModel Get(int id)
        {
            return repo.GetById(id);
        }

        [Authorize(Policy = "PostAttendee")]
        [HttpPost("Post/{conferenceId}/{name}")]
        public IActionResult Post(int conferenceId, string name)
        {
            var attendee = repo.Add(new AttendeeModel { ConferenceId = conferenceId, Name = name });
            return new CreatedAtActionResult("Get", "Attendee", new { id = attendee.Id }, attendee);
        }

        [HttpGet("GetAttendeesTotal/{conferenceId}")]
        public int GetAttendeesTotal(int conferenceId)
        {
            return repo.GetAttendeesTotal(conferenceId);
        }
    }
}
