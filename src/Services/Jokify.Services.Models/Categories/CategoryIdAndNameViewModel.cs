using System.Linq;
using AutoMapper;
using Jokify.Data.Models;
using Jokify.Services.Mapping;

namespace Jokify.Services.Models.Categories
{
    public class CategoryIdAndNameViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAndCount => 
            $"{this.Name} ({this.CountOfAllJokes})";

        // JokesCount
        public int CountOfAllJokes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryIdAndNameViewModel>()
                .ForMember(x => x.CountOfAllJokes,
                    m => m.MapFrom(c => c.Jokes.Count()));
        }
    }
}
