using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jokify.Data.Common;
using Jokify.Data.Models;
using Jokify.Services.Mapping;
using Jokify.Services.Models.Home;
using Jokify.Services.Models.Jokes;

namespace Jokify.Services.DataServices
{
    public class JokesService : IJokesService
    {
        private readonly IRepository<Joke> jokesRepository;
        private readonly IRepository<Category> categoriesRepository;

        public JokesService(
            IRepository<Joke> jokesRepository,
            IRepository<Category> categoriesRepository)
        {
            this.jokesRepository = jokesRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var jokes = this.jokesRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .To<IndexJokeViewModel>().Take(count).ToList();

            return jokes;
        }

        public int GetCount()
        {
            return this.jokesRepository.All().Count();
        }

        public async Task<int> Create(int categoryId, string content)
        {
            var joke = new Joke
            {
                CategoryId = categoryId,
                Content = content,
            };

            await this.jokesRepository.AddAsync(joke);
            await this.jokesRepository.SaveChangesAsync();

            return joke.Id;
        }

        public TViewModel GetJokeById<TViewModel>(int id)
        {
            var joke = this.jokesRepository.All().Where(x => x.Id == id)
                .To<TViewModel>().FirstOrDefault();
            return joke;
        }

        public IEnumerable<JokeSimpleViewModel> GetAllByCategory(int categoryId) 
            => this.jokesRepository
                    .All()
                    .Where(j => j.CategoryId == categoryId)
                    .To<JokeSimpleViewModel>();

        public bool AddRatingToJoke(int jokeId, int rating)
        {
            var joke = this.jokesRepository.All().FirstOrDefault(j => j.Id == jokeId);
            if (joke != null)
            {
                joke.Rating += rating;
                this.jokesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
