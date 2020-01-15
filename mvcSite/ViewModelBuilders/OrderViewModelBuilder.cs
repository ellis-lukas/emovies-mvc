using mvcSite.Models;
using mvcSite.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.ViewModelBuilders
{
    public class OrderViewModelBuilder
    {
        public OrderViewModel BuildOrderViewModel()
        {
            return new OrderViewModel();
        }

        public Customer BuildCustomerFromOrderViewModel(OrderViewModel orderViewModel)
        {
            Customer customerForOrderViewModel = new Customer
            {
                Name = orderViewModel.Name,
                Email = orderViewModel.Email,
                CardNumber = orderViewModel.CardNumber,
                CardType = orderViewModel.CardType.ToString(),
                FuturePromotions = orderViewModel.FuturePromotions
            };

            return customerForOrderViewModel;
        }
    }
}