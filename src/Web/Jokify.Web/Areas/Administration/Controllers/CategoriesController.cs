using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jokify.Data.Common;
using Jokify.Data.Models;
using Jokify.Services.DataServices;
using Jokify.Web.Areas.Administration.Models.Categories;
using Jokify.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jokify.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdministrationBaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRepository<Category> categories;

        public CategoriesController(
            ICategoriesService categoriesService, IRepository<Category> categories)
        {
            this.categoriesService = categoriesService;
            this.categories = categories;
        }

        public IActionResult Index()
        {
            var categories = categoriesService
                .GetAll()
                .ToList();

            return this.View(categories);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            var newCategory = Mapper.Map<Category>(model);
            await this.categories.AddAsync(newCategory);
            await this.categories.SaveChangesAsync();
            return this.RedirectToAction(nameof(Index));
        }

    }
}
