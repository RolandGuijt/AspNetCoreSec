using System.Collections.Generic;
using AspNetSecurity_m4_Api.Repositories;
using AspNetSecurity_m4_Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m4_Api.Controllers
{
    [Authorize]
    public class ConferenceController: Controller
    {
        private readonly ConferenceRepo repo;

        public ConferenceController(ConferenceRepo repo)
        {
           
            this.repo = repo;
        }

        public IEnumerable<ConferenceModel> GetAll()
        {
            return repo.GetAll();
        }

        [HttpPost]
        public void Add([FromBody]ConferenceModel conference)
        {
            repo.Add(conference);
        }
    }
}
