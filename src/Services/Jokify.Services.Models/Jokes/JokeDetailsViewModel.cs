using Jokify.Data.Models;
using Jokify.Services.Mapping;

namespace Jokify.Services.Models.Jokes
{
    public class JokeDetailsViewModel : IMapFrom<Joke>
    {
        public string Content { get; set; }

        public string CategoryName { get; set; }
    }
}
