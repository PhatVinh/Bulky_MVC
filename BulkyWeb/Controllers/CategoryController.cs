using Bulky.DataAccess.Data;
using Bulky.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Category Name cannot be the same with Display Order");
            //}
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category edit successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Category category = _context.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category edit successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int Id)
        {
            Category category = _context.Categories.FirstOrDefault(u => u.Id == Id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category delete success";
            return RedirectToAction("Index");
        }
    }
}
