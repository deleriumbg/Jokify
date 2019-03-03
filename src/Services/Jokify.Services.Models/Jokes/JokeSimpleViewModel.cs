using Jokify.Data.Models;
using Jokify.Services.Mapping;

namespace Jokify.Services.Models.Jokes
{
    public class JokeSimpleViewModel : IMapFrom<Joke>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
