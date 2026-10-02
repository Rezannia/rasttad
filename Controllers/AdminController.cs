using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rasttad.Data;
using Rasttad.Models;

namespace Rasttad.Controllers
{
    [Authorize] // فقط کاربران وارد شده دسترسی دارند
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
            // آمار برای داشبورد
            ViewBag.TotalLeads = await _context.Leads.CountAsync();
            ViewBag.TotalIndustries = await _context.Industries.CountAsync();
            ViewBag.TotalChallenges = await _context.Challenges.CountAsync();
            ViewBag.TotalServices = await _context.Services.CountAsync();

            // سرنخ‌های اخیر (۵ مورد آخر)
            ViewBag.RecentLeads = await _context.Leads
                .OrderByDescending(l => l.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View();
        }

        // ==================== مدیریت سرنخ‌ها ====================

        // لیست سرنخ‌ها
        public async Task<IActionResult> Leads(string search)
        {
            var query = _context.Leads.AsQueryable();

            // اگر کاربر جستجو کرد
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

        // مشاهده جزئیات یک سرنخ
        public async Task<IActionResult> LeadDetails(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }
            return View(lead);
        }

        // حذف سرنخ (POST)
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

        // علامت‌گذاری سرنخ به عنوان پیگیری شده
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

        // لیست صنایع
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

        // فرم ویرایش صنعت (GET)
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

        // ذخیره تغییرات صنعت (POST)
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

        // فرم اضافه کردن صنعت جدید (GET)
        public IActionResult CreateIndustry()
        {
            return View();
        }

        // ذخیره صنعت جدید (POST)
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

        // حذف صنعت (POST)
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
                // حذف چالش‌ها، خدمات و نمونه‌کارهای مرتبط
                _context.Challenges.RemoveRange(industry.Challenges);
                _context.Services.RemoveRange(industry.Services);
                _context.CaseStudies.RemoveRange(industry.CaseStudies);
                _context.Industries.Remove(industry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Industries");
        }
    }
}