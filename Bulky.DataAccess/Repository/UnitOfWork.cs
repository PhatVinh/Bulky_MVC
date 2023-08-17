using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public ICategoryRepository CategoryRepository { get; private set; }
		public IProductRepository ProductRepository { get; private set; }
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			CategoryRepository = new CategoryRepository(context);
			ProductRepository = new ProductRepository(context);
		}


		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
