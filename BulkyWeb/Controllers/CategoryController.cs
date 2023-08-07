using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
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
				_unitOfWork.CategoryRepository.Add(category);
				_unitOfWork.SaveChanges();
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

            Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == Id);
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
                _unitOfWork.CategoryRepository.Update(category);
				_unitOfWork.SaveChanges();
                TempData["success"] = "Category edit successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int Id)
        {
            Category category = _unitOfWork.CategoryRepository.Get(u => u.Id == Id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
			_unitOfWork.CategoryRepository.Remove(category);
			_unitOfWork.SaveChanges();
            TempData["success"] = "Category delete success";
            return RedirectToAction("Index");
        }
    }
}
