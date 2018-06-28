using System.Collections.Generic;
using System.Linq;
using AspNetSecurity_m3.Models;

namespace AspNetSecurity_m3.Repositories
{  
    public class ProposalRepo
    {
        private readonly List<ProposalModel> proposals = new List<ProposalModel>();

        public ProposalRepo()
        {
            proposals.Add(new ProposalModel
            {
                Id = 1,
                ConferenceId = 1,
                Speaker = "Roland Guijt",
                Title = "Understanding ASP.NET Core Security"
            });
            proposals.Add(new ProposalModel
            {
                Id = 2,
                ConferenceId = 2,
                Speaker = "John Reynolds",
                Title = "Starting Your Developer Career"
            });
            proposals.Add(new ProposalModel
            {
                Id = 3,
                ConferenceId = 2,
                Speaker = "Stan Lipens",
                Title = "ASP.NET Core TagHelpers"
            });
        }

        public IEnumerable<ProposalModel> GetAllForConference(int conferenceId)
        {
            return proposals.Where(p => p.ConferenceId == conferenceId);
        }

        public IEnumerable<ProposalModel> GetAllApprovedForConference(int conferenceId)
        {
            return GetAllForConference(conferenceId).Where(p => p.Approved);
        }

        public void Add(ProposalModel model)
        {
            model.Id = proposals.Max(p => p.Id) + 1;
            proposals.Add(model);
        }

        public ProposalModel Approve(int proposalId)
        {
            var proposal = proposals.First(p => p.Id == proposalId);
            proposal.Approved = true;
            return proposal;
        }
    }
}
