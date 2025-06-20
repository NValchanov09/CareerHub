using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerHub.Data;
using CareerHub.Models;
using Microsoft.AspNetCore.Hosting;

namespace CareerHub.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LanguagesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: Languages
        public IActionResult Index(int page = 1, int pageSize = 10, string sortBy = "id", bool isDescending = false)
        {
            var languages = _context.Language.AsQueryable();

            List<int> pageSizes = new List<int>{ 5, 10, 15, 25, 50, 100 };

            int itemCount = languages.Count();

            var pageSizeOptions = new List<SelectListItem>();

            foreach (int size in pageSizes)
            {
                if (size < itemCount)
                {
                    pageSizeOptions.Add(new SelectListItem { Value = $"{size}", Text = $"{size}", Selected = pageSize == size });
                }
            }

            pageSizeOptions.Add(new SelectListItem { Value = $"{itemCount}", Text = $"All({itemCount})", Selected = pageSize == itemCount });

            switch(sortBy)
            {
                case "name":
                    languages = languages.OrderBy(l => l.Name);
                    break;

                default:
                    languages = languages.OrderBy(l => l.Id);
                    break;
            }

            if (isDescending)
                languages = languages.Reverse();

            if (page < 1 || page > itemCount)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            if(pageSize > itemCount && itemCount > 0)
                pageSize = itemCount;

            if ((page - 1) * pageSize > itemCount)
                page = 1;

            int skip = (page - 1) * pageSize;

            int startItemsShowing = skip + 1;

            int endItemsShowing = startItemsShowing + pageSize - 1;

            Pager pager = new Pager(itemCount, page, pageSize, startItemsShowing, endItemsShowing, pageSizeOptions, sortBy, isDescending);

            var data = languages.Skip(skip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Language
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(stream);
                }

                language.LogoPath = "/images/" + uniqueFileName;
            }

            _context.Add(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Language.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Language language, IFormFile logoFile)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (logoFile != null && logoFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await logoFile.CopyToAsync(stream);
                        }

                        language.LogoPath = "/images/" + uniqueFileName;
                    }

                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Language
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await _context.Language.FindAsync(id);
            if (language != null)
            {
                _context.Language.Remove(language);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
        {
            return _context.Language.Any(e => e.Id == id);
        }
    }
}
