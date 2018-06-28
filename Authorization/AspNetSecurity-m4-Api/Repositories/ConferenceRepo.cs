using System;
using System.Collections.Generic;
using System.Linq;
using AspNetSecurity_m4_Shared.Models;

namespace AspNetSecurity_m4_Api.Repositories
{
    public class ConferenceRepo
    {
        private readonly List<ConferenceModel> conferences = new List<ConferenceModel>();

        public ConferenceRepo()
        {
            conferences.Add(new ConferenceModel { Id = 1, Name = "NDC", Location = "Oslo", Start = new DateTime(2017, 6, 12)});
            conferences.Add(new ConferenceModel { Id = 2, Name = "IT/DevConnections", Location = "San Francisco", Start = new DateTime(2017, 10, 18)});
        }
        public IEnumerable<ConferenceModel> GetAll()
        {
            return conferences;
        }

        public ConferenceModel GetById(int id)
        {
            return conferences.First(c => c.Id == id);
        }

        public void Add(ConferenceModel model)
        {
            model.Id = conferences.Max(c => c.Id) + 1;
            conferences.Add(model);
        }
    }
}
