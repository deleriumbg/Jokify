using Jokify.Data.Models;
using Jokify.Services.Mapping;

namespace Jokify.Web.Areas.Administration.Models.Categories
{
    public class CreateCategoryInputModel : IMapTo<Category>
    {
        public string Name { get; set; }
    }
}
