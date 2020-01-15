using mvcSite.ViewModelBuilders;
using mvcSite.ViewModels.Summary;
using System.Web.Mvc;

namespace mvcSite.Controllers
{
    public class SummaryController : Controller
    {
        private readonly SummaryViewModelBuilder _summaryViewModelBuilder;

        public SummaryController(SummaryViewModelBuilder summaryViewModelBuilder)
        {
            _summaryViewModelBuilder = summaryViewModelBuilder;
        }

        public ActionResult Index()
        {
            SummaryViewModel summaryDisplayData = _summaryViewModelBuilder.BuildSummaryViewModel();

            return View(summaryDisplayData);
        }
    }
}