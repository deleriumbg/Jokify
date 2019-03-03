using System.Collections.Generic;
using Jokify.Services.Models.Categories;

namespace Jokify.Services.DataServices
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryIdAndNameViewModel> GetAll();

        bool IsCategoryIdValid(int categoryId);

        int? GetCategoryId(string name);
    }
}
