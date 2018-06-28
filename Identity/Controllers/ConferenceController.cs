using AspNetSecurity_m3.Models;
using AspNetSecurity_m3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m3.Controllers
{
    [Authorize]
    public class ConferenceController: Controller
    {
        private readonly ConferenceRepo repo;

        public ConferenceController(ConferenceRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Organizer - Conference Overview";
            return View(repo.GetAll());
            
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Organizer - Add Conference";
            return View(new ConferenceModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ConferenceModel model)
        {
            if (ModelState.IsValid)
                repo.Add(model);

            return RedirectToAction("Index");
        }
    }
}
