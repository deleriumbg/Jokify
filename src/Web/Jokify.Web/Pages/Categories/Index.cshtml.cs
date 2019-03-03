using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jokify.Services.DataServices;
using Jokify.Services.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jokify.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoriesService categoriesService;

        public IndexModel(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IEnumerable<CategoryIdAndNameViewModel> Categories { get; set; }

        public void OnGet()
        {
            this.Categories = this.categoriesService.GetAll();
        }
    }
}