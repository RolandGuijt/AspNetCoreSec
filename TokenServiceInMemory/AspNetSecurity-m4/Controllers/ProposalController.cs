using System.Threading.Tasks;
using AspNetSecurity_m4.Api;
using AspNetSecurity_m4_Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m4.Controllers
{
    [Authorize]
    public class ProposalController: Controller
    {
        private readonly ConferenceApiService conferenceService;
        private readonly ProposalApiService proposalService;

        public ProposalController(ConferenceApiService conferenceService, ProposalApiService proposalService)
        {
            this.conferenceService = conferenceService;
            this.proposalService = proposalService;
        }

        public async Task<IActionResult> Index(int conferenceId)
        {
            var conference = await conferenceService.GetById(conferenceId);      
            ViewBag.Title = $"Speaker - Proposals For Conference {conference.Name} {conference.Location}";
            ViewBag.ConferenceId = conferenceId;

            return View(await proposalService.GetAllForConference(conferenceId));
        }

        public IActionResult AddProposal(int conferenceId)
        {
            ViewBag.Title = "Speaker - Add Proposal";
            return View(new ProposalModel {ConferenceId = conferenceId});
        }

        [HttpPost]
        public async Task<IActionResult> AddProposal(ProposalModel proposal)
        {
            if (ModelState.IsValid)
                await proposalService.Add(proposal);
            return RedirectToAction("Index", new {conferenceId = proposal.ConferenceId});
        }

        public async Task<IActionResult> Approve(int proposalId)
        {
            var proposal = await proposalService.Approve(proposalId);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }
    }
}
