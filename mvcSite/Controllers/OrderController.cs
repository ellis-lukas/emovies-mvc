using Autofac;
using Autofac.Integration.Mvc;
using mvcSite.DAL;
using mvcSite.DAL.DatabaseAccess;
using mvcSite.Models;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModelBuilders;
using mvcSite.ViewModels.Order;
using System.Collections.Generic;
using System.Web.Mvc;

namespace mvcSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderCreationService _orderCreationService;
        private readonly SessionManager _sessionManager;
        private readonly OrderViewModelBuilder _orderViewModelBuilder;

        public OrderController(OrderViewModelBuilder orderViewModelBuilder,
            OrderCreationService orderCreationService,
            SessionManager sessionManager)
        {
            _orderViewModelBuilder = orderViewModelBuilder;
            _orderCreationService = orderCreationService;
            _sessionManager = sessionManager;
        }

        // GET: Order
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> cardTypeSelectList = new List<SelectListItem>() { 
                    new SelectListItem() { Text = "Mastercard", Value = "Mastercard" },
                    new SelectListItem() { Text = "Visa", Value = "Visa" },
                    new SelectListItem() { Text = "American Express", Value = "American Express" },
                    new SelectListItem() { Text = "Discover", Value = "Discover" }
                };

            ViewBag.CardTypeSelectList = cardTypeSelectList;
            OrderViewModel orderViewModel = _orderViewModelBuilder.BuildOrderViewModel();

            return View(orderViewModel);
        }

        // POST: Order
        [HttpPost]
        public ActionResult Index(OrderViewModel orderData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer customerForOrderViewModel = _orderViewModelBuilder.BuildCustomerFromOrderViewModel(orderData);
                    IEnumerable<OrderLine> nonZeroOrderLines = _sessionManager.OrderLines;
                    decimal orderTotal = _sessionManager.Total;

                    StagedDataForWriting stagedDataForWriting = new StagedDataForWriting
                    {
                        Customer = customerForOrderViewModel,
                        OrderLines = nonZeroOrderLines,
                        Total = orderTotal
                    };

                    _orderCreationService.CreateOrders(stagedDataForWriting);
                    _sessionManager.CustomerData = customerForOrderViewModel;

                    return RedirectToAction("Index", "Summary");
                }
                catch
                {
                    IEnumerable<SelectListItem> cardTypeSelectList = new List<SelectListItem>() {
                        new SelectListItem() { Text = "Mastercard", Value = "Mastercard" },
                        new SelectListItem() { Text = "Visa", Value = "Visa" },
                        new SelectListItem() { Text = "American Express", Value = "American Express" },
                        new SelectListItem() { Text = "Discover", Value = "Discover" }
                    };

                    ViewBag.CardTypeSelectList = cardTypeSelectList;
                    return View(orderData);
                }
            }
            else
            {
                IEnumerable<SelectListItem> cardTypeSelectList = new List<SelectListItem>() {
                    new SelectListItem() { Text = "Mastercard", Value = "Mastercard" },
                    new SelectListItem() { Text = "Visa", Value = "Visa" },
                    new SelectListItem() { Text = "American Express", Value = "American Express" },
                    new SelectListItem() { Text = "Discover", Value = "Discover" }
                };

                ViewBag.CardTypeSelectList = cardTypeSelectList;
                return View(orderData);
            }
        }
    }
}