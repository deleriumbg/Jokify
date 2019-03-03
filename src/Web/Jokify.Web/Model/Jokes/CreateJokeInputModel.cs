using System.ComponentModel.DataAnnotations;
using Jokify.Services.Models.Jokes;

namespace Jokify.Web.Model.Jokes
{
    public class CreateJokeInputModel
    {
        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [ValidCategoryId]
        public int CategoryId { get; set; }
    }
}
