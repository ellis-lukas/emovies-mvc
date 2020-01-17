using mvcSite.ViewModelBuilders;
using mvcSite.ViewModels.Home;
using System.Web.Mvc;

namespace mvcSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeViewModelBuilder _homeViewModelBuilder;

        public HomeController(HomeViewModelBuilder homeViewModelBuilder)
        {
            _homeViewModelBuilder = homeViewModelBuilder;
        }

        //GET: Home
        public ActionResult Index()
        {
            HomeViewModel initialisedHomeViewModel = _homeViewModelBuilder.BuildInitialisedHomeViewModel();

            return View(initialisedHomeViewModel); 
        }

        //POST: Home
        [HttpPost]
        public ActionResult Index(HomeViewModel movieOrderData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _homeViewModelBuilder.SaveMovieOrderDataFromHomeViewModel(movieOrderData);

                    return RedirectToAction("Index", "Order");
                }
                catch
                {
                    return View(movieOrderData);
                }
            }
            else
            {
                return View(movieOrderData);
            }
        }
    }
}