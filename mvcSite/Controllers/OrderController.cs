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
                    ValidateModel(orderData);

                    Customer customerForOrderViewModel = _orderViewModelBuilder.BuildCustomerFromOrderViewModel(orderData);
                    IEnumerable<OrderLine> nonZeroOrderLines = _sessionManager.NonZeroOrderLines;

                    StagedDataForWriting stagedDataForWriting = new StagedDataForWriting
                    {
                        Customer = customerForOrderViewModel,
                        OrderLines = nonZeroOrderLines
                    };

                    _orderCreationService.CreateOrders(stagedDataForWriting);
                    _sessionManager.CustomerData = customerForOrderViewModel;

                    return RedirectToAction("Index", "Summary");
                }
                catch
                {
                    return View(orderData);
                }
            }
            else
            {
                return View(orderData);
            }
        }
    }
}