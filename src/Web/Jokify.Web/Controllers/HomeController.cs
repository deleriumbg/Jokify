using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jokify.Services.DataServices;
using Jokify.Services.Models;
using Jokify.Services.Models.Home;

namespace Jokify.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IJokesService jokesService;

        public HomeController(IJokesService jokesService)
        {
            this.jokesService = jokesService;
        }
        
        public IActionResult Index(int id)
        {
            var jokes = this.jokesService.GetRandomJokes(24);
            var viewModel = new IndexViewModel
            {
                Jokes = jokes,
            };

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"Currently Jokify has {this.jokesService.GetCount()} jokes.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
