using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rasttad.Data;
using Rasttad.Models;

namespace Rasttad.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==================== داشبورد ====================
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalLeads = await _context.Leads.CountAsync();
            ViewBag.TotalIndustries = await _context.Industries.CountAsync();
            ViewBag.TotalChallenges = await _context.Challenges.CountAsync();
            ViewBag.TotalServices = await _context.Services.CountAsync();

            ViewBag.RecentLeads = await _context.Leads
                .OrderByDescending(l => l.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View();
        }

        // ==================== مدیریت سرنخ‌ها ====================

        public async Task<IActionResult> Leads(string search)
        {
            var query = _context.Leads.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(l =>
                    l.FullName.Contains(search) ||
                    l.Phone.Contains(search) ||
                    (l.Email != null && l.Email.Contains(search)) ||
                    (l.IndustrySlug != null && l.IndustrySlug.Contains(search)));
            }

            var leads = await query
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            ViewBag.SearchTerm = search;
            return View(leads);
        }

        public async Task<IActionResult> LeadDetails(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }
            return View(lead);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead != null)
            {
                _context.Leads.Remove(lead);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Leads");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFollowUp(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead != null)
            {
                lead.IsFollowedUp = !lead.IsFollowedUp;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Leads");
        }

        // ==================== مدیریت صنایع ====================

        public async Task<IActionResult> Industries()
        {
            var industries = await _context.Industries
                .Include(i => i.Challenges)
                .Include(i => i.Services)
                .Include(i => i.CaseStudies)
                .OrderBy(i => i.Id)
                .ToListAsync();

            return View(industries);
        }

        public async Task<IActionResult> EditIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Challenges)
                .Include(i => i.Services)
                .Include(i => i.CaseStudies)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIndustry(Industry industry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Industries.Update(industry);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Industries");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Industries.Any(i => i.Id == industry.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(industry);
        }

        public IActionResult CreateIndustry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIndustry(Industry industry)
        {
            if (ModelState.IsValid)
            {
                _context.Industries.Add(industry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Industries");
            }
            return View(industry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Challenges)
                .Include(i => i.Services)
                .Include(i => i.CaseStudies)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (industry != null)
            {
                _context.Challenges.RemoveRange(industry.Challenges);
                _context.Services.RemoveRange(industry.Services);
                _context.CaseStudies.RemoveRange(industry.CaseStudies);
                _context.Industries.Remove(industry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Industries");
        }

        // ==================== مدیریت محتوای صنعت ====================

        public async Task<IActionResult> IndustryContent(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Challenges)
                .Include(i => i.Services)
                .Include(i => i.CaseStudies)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        // ==================== چالش‌ها ====================

        public IActionResult AddChallenge(int industryId)
        {
            ViewBag.IndustryId = industryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChallenge(Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                _context.Challenges.Add(challenge);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = challenge.IndustryId });
            }
            ViewBag.IndustryId = challenge.IndustryId;
            return View(challenge);
        }

        public async Task<IActionResult> EditChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }
            return View(challenge);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChallenge(Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                _context.Challenges.Update(challenge);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = challenge.IndustryId });
            }
            return View(challenge);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                int industryId = challenge.IndustryId;
                _context.Challenges.Remove(challenge);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = industryId });
            }
            return RedirectToAction("Industries");
        }

        // ==================== خدمات ====================

        public IActionResult AddService(int industryId)
        {
            ViewBag.IndustryId = industryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = service.IndustryId });
            }
            ViewBag.IndustryId = service.IndustryId;
            return View(service);
        }

        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = service.IndustryId });
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                int industryId = service.IndustryId;
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = industryId });
            }
            return RedirectToAction("Industries");
        }

        // ==================== نمونه‌کارها ====================

        public IActionResult AddCaseStudy(int industryId)
        {
            ViewBag.IndustryId = industryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCaseStudy(CaseStudy caseStudy)
        {
            if (ModelState.IsValid)
            {
                _context.CaseStudies.Add(caseStudy);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = caseStudy.IndustryId });
            }
            ViewBag.IndustryId = caseStudy.IndustryId;
            return View(caseStudy);
        }

        public async Task<IActionResult> EditCaseStudy(int id)
        {
            var caseStudy = await _context.CaseStudies.FindAsync(id);
            if (caseStudy == null)
            {
                return NotFound();
            }
            return View(caseStudy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCaseStudy(CaseStudy caseStudy)
        {
            if (ModelState.IsValid)
            {
                _context.CaseStudies.Update(caseStudy);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = caseStudy.IndustryId });
            }
            return View(caseStudy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCaseStudy(int id)
        {
            var caseStudy = await _context.CaseStudies.FindAsync(id);
            if (caseStudy != null)
            {
                int industryId = caseStudy.IndustryId;
                _context.CaseStudies.Remove(caseStudy);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndustryContent", new { id = industryId });
            }
            return RedirectToAction("Industries");
        }
    }
}