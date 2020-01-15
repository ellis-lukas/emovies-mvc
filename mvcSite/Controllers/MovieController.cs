using mvcSite.ViewModelBuilders;
using mvcSite.ViewModels.Movie;
using System.Web.Mvc;

namespace mvcSite.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieViewModelBuilder _movieViewModelBuilder;

        public MovieController(MovieViewModelBuilder movieViewModelBuilder)
        {
            _movieViewModelBuilder = movieViewModelBuilder;
        }

        // GET: Movie
        public ActionResult Index(int id)
        {
            MovieViewModel movieDataForDescription = _movieViewModelBuilder.BuildMovieViewModel(id);

            return View(movieDataForDescription);
        }
    }
}