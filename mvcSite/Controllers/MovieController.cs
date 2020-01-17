using mvcSite.Models;
using mvcSite.Repositories;
using mvcSite.ViewModelBuilders;
using mvcSite.ViewModels.Movie;
using System.Web.Mvc;
using mvcSite.UrlFriendlyConfiguration;

namespace mvcSite.Controllers
{
    public class MovieController : Controller//Movie/name ie no index in url. not good for search engines.
    {
        private readonly MovieViewModelBuilder _movieViewModelBuilder;
        private readonly MovieRepository _movieRepository;

        public MovieController(MovieViewModelBuilder movieViewModelBuilder, MovieRepository movieRepository)
        {
            _movieViewModelBuilder = movieViewModelBuilder;
            _movieRepository = movieRepository;
        }

        // GET: Movie
        public ActionResult Index(int id, string movieName)
        {
            MovieViewModel movieDataForDescription = _movieViewModelBuilder.BuildMovieViewModel(id);
            if(movieDataForDescription == null)
            {
                return HttpNotFound();
            }

            Movie movieSelected = _movieRepository.GetMovieByID(id);
            string expectedName = movieSelected.Name.ToSeoUrl();
            string actualName = (movieName ?? "").ToLower();

            if( expectedName != actualName)
            {
                RedirectToActionPermanent("Index", new { id = id, movieName = expectedName});
            }

            return View(movieDataForDescription);
        }

        // POST: Movie
        [HttpPost]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}