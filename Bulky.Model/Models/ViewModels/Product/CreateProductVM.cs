using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Bulky.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Model.Models.ViewModels
{
    public class CreateProductVM
    {
		public Product Product { get; set; }

		public IEnumerable<SelectListItem> Category { get; set; }
	}
}
