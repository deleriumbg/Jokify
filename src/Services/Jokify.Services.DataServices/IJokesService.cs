using System.Collections.Generic;
using System.Threading.Tasks;
using Jokify.Data.Models;
using Jokify.Services.Models.Home;
using Jokify.Services.Models.Jokes;

namespace Jokify.Services.DataServices
{
    public interface IJokesService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();

        Task<int> Create(int categoryId, string content);

        TViewModel GetJokeById<TViewModel>(int id);

        IEnumerable<JokeSimpleViewModel> GetAllByCategory(int categoryId);

        bool AddRatingToJoke(int jokeId, int rating);
    }
}