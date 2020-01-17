using mvcSite.Models;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModels.Summary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static mvcSite.ViewModels.Summary.SummaryViewModel;

namespace mvcSite.ViewModelBuilders
{
    public class SummaryViewModelBuilder
    {
        private readonly MovieRepository _movieRepository;
        private readonly SessionManager _sessionManager;

        public SummaryViewModelBuilder(MovieRepository movieRepository, SessionManager sessionManager)
        {
            _movieRepository = movieRepository;

            _sessionManager = sessionManager;
        }

        public SummaryViewModel BuildSummaryViewModel()
        {
            Customer customerData = _sessionManager.CustomerData;
            decimal orderTotal = _sessionManager.Total;

            IEnumerable<SummaryRow> summaryRows = BuildSummaryRows();

            SummaryViewModel summaryViewModel = new SummaryViewModel
            {
                Name = customerData.Name,
                Email = customerData.Email,
                CardNumber = CardNumberProtectedFormat(customerData.CardNumber),
                CardType = customerData.CardType,
                FuturePromotions = customerData.FuturePromotions ? YesNo.Yes : YesNo.No,
                SummaryRows = summaryRows,
                Total = orderTotal
            };

            return summaryViewModel;
        }

        public IEnumerable<SummaryRow> BuildSummaryRows()
        {
            IEnumerable<OrderLine> orderData = _sessionManager.OrderLines;
            List<SummaryRow> summaryRows = new List<SummaryRow>();
            
            foreach(OrderLine orderLine in orderData)
            {
                int IDOfCorrespondingMovie = orderLine.MovieID;
                Movie correspondingMovie = _movieRepository.GetMovieByID(IDOfCorrespondingMovie);

                SummaryRow summaryRowForOrderLine = new SummaryRow
                {
                    Name = correspondingMovie.Name,
                    Price = correspondingMovie.Price,
                    Quantity = orderLine.Quantity
                };

                summaryRows.Add(summaryRowForOrderLine);
            }

            return summaryRows;
        }

        private string CardNumberProtectedFormat(string cardNumber)
        {
            string starredForm = new String('*', cardNumber.Length);
            string firstFourNumbers = cardNumber.Substring(0, 4);
            string protectedForm = firstFourNumbers + starredForm.Substring(4);
            protectedForm = Regex.Replace(protectedForm, ".{4}", "$0 ");
            return protectedForm;
        }
    }
}