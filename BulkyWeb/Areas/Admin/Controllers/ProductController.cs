using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Model.Models;
using Bulky.Model.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll().ToList();

			return View(products);
		}
		public IActionResult Create()
		{
			CreateProductVM productVm = new CreateProductVM()
			{
				Category = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem()
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};
			return View(productVm);
		}
		[HttpPost]
		public IActionResult Create(CreateProductVM productVm)
		{
			if (!ModelState.IsValid)
			{
				productVm.Category = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem()
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(productVm);
			}
			Product product = productVm.Product;
			_unitOfWork.ProductRepository.Add(product);
			_unitOfWork.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
