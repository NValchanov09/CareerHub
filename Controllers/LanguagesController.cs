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
using System.Diagnostics;

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
        public IActionResult Index(int page = 1, int pageSize = 10, string sortBy = "id", bool isDescending = false, string name = "")
        {
            var languages = _context.Language.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                languages = languages.Where(l => l.Name.Contains(name));
            }

            int itemCount = languages.Count();

            var pageSizeOptions = new List<SelectListItem>();

            List<int> pageSizes = new List<int> { 5, 10, 15, 25, 50, 100 };

            foreach (int size in pageSizes)
            {
                if (size < itemCount)
                {
                    pageSizeOptions.Add(new SelectListItem { Value = $"{size}", Text = $"{size}", Selected = pageSize == size });
                }
            }

            pageSizeOptions.Add(new SelectListItem { Value = $"{itemCount}", Text = $"All ({itemCount})", Selected = pageSize == itemCount });

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

            if (pageSize < 1)
                pageSize = 10;

            if(pageSize > itemCount)
                pageSize = itemCount;

            int skip = (page - 1) * pageSize;

            int startItemsShowing = skip + 1;

            int endItemsShowing = startItemsShowing + pageSize - 1;

            Pager pager = new Pager(itemCount, page, pageSize, startItemsShowing, endItemsShowing, pageSizeOptions, sortBy, isDescending);

            LanguagesSearchViewModel viewModel = new LanguagesSearchViewModel(name);

            this.ViewBag.Pager = pager;
            this.ViewBag.LanguagesSearchViewModel = viewModel;

            var data = languages.Skip(skip).Take(pager.PageSize).ToList();

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
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/languages");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(stream);
                }

                language.LogoPath = "/images/languages/" + uniqueFileName;
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
        public async Task<IActionResult> Edit(int id, Language language, IFormFile? logoFile)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string oldLogoPath = await _context.Language.AsNoTracking().Where(l => l.Id == id).Select(l => l.LogoPath).FirstOrDefaultAsync();

                    if (logoFile != null && logoFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/languages");
                        Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await logoFile.CopyToAsync(stream);
                        }

                        language.LogoPath = "/images/languages/" + uniqueFileName;

                        DeleteImage(oldLogoPath);
                    }
                    else
                    {
                        language.LogoPath = oldLogoPath;
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
                DeleteImage(language.LogoPath);
                _context.Language.Remove(language);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSelected([FromBody] int[] languageIds)
        {
            if (languageIds == null)
            {
                return Json(new { success = false, message = "languageIds is null." });
            }
            if (languageIds.Length == 0)
            {
                return Json(new { success = false, message = "No languages selected for deletion." });
            }

            try
            {
                var languagesToDelete = _context.Language.Where(c => languageIds.Contains(c.Id)).ToList();

                foreach(var language in languagesToDelete)
                {
                    DeleteImage(language.LogoPath);
                }

                _context.Language.RemoveRange(languagesToDelete);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting languages: " + ex.Message });
            }
        }

        private bool LanguageExists(int id)
        {
            return _context.Language.Any(e => e.Id == id);
        }

        private void DeleteImage(string? imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to delete image: {ex.Message}");
                    }
                }
                else
                {
                    Debug.WriteLine($"Image not found at: {path}");
                }
            }
        }
    }
}
