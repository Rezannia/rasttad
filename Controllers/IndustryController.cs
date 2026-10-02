using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rasttad.Data;
using Rasttad.Models;

namespace Rasttad.Controllers
{
    public class IndustryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndustryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(string slug, string employees, string location)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return RedirectToAction("Index", "Home");
            }

            var industry = _context.Industries
                .Include(i => i.Challenges)
                .Include(i => i.Services)
                .Include(i => i.CaseStudies)
                .FirstOrDefault(i => i.Slug == slug);

            if (industry == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Employees"] = employees;
            ViewData["Location"] = location;

            return View(industry);
        }

        [HttpPost]
        public IActionResult SubmitLead(Lead lead)
        {
            if (ModelState.IsValid)
            {
                lead.CreatedAt = DateTime.Now;
                _context.Leads.Add(lead);
                _context.SaveChanges();

                return RedirectToAction("ThankYou", new { industry = lead.IndustrySlug });
            }

            return RedirectToAction("Details", new { slug = lead.IndustrySlug });
        }

        public IActionResult ThankYou(string industry)
        {
            ViewData["IndustrySlug"] = industry;
            return View();
        }
    }
}