using mvcSite.Models;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModels.Home;
using System.Collections.Generic;
using System.Linq;

namespace mvcSite.ViewModelBuilders
{
    public class HomeViewModelBuilder
    {
        private readonly MovieRepository _movieRepository;
        private readonly SessionManager _sessionManager;

        public HomeViewModelBuilder(MovieRepository movieRepository, SessionManager sessionManager)
        {
            _movieRepository = movieRepository;
            _sessionManager = sessionManager;
        }

        public HomeViewModel BuildInitialisedHomeViewModel()
        {
            List<MovieOrderRow> movieOrderRowsWithZeroAsQuantityOrdered = BuildMovieOrderRowsWithZeroAsQuantityOrdered();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                MovieOrderRows = movieOrderRowsWithZeroAsQuantityOrdered
            };

            return homeViewModel;
        }

        private List<MovieOrderRow> BuildMovieOrderRowsWithZeroAsQuantityOrdered()
        {
            IEnumerable<Movie> movies = _movieRepository.GetMovies();
            List<MovieOrderRow> moviesWithZeroAsQuantityOrdered = new List<MovieOrderRow>();

            foreach (Movie movie in movies)
            {
                MovieOrderRow movieOrderRowWithZeroAsQuantity = new MovieOrderRow
                {
                    MovieID = movie.ID,
                    Name = movie.Name,
                    Price = movie.Price,
                    Quantity = 0
                };

                moviesWithZeroAsQuantityOrdered.Add(movieOrderRowWithZeroAsQuantity);
            }

            return moviesWithZeroAsQuantityOrdered;
        }

        public void SaveMovieOrderDataFromHomeViewModel(HomeViewModel movieOrderData)
        {
            IEnumerable<OrderLine> dataAsOrderLines = ConvertHomeViewModelToOrderLines(movieOrderData);
            UpdatePricesToOfficialValues(dataAsOrderLines);
            IEnumerable<OrderLine> nonZeroQuantityOrderLines = RemoveZeroQuantityOrderLines(dataAsOrderLines);

            decimal orderTotal = TotalOrderLines(dataAsOrderLines);

            _sessionManager.OrderLines = nonZeroQuantityOrderLines;
            _sessionManager.Total = orderTotal;
        }

        private IEnumerable<OrderLine> ConvertHomeViewModelToOrderLines(HomeViewModel homeViewModel)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            IEnumerable<MovieOrderRow> movieOrderRows = homeViewModel.MovieOrderRows;

            foreach (MovieOrderRow movieOrderRow in movieOrderRows)
            {
                OrderLine orderLineForRow = new OrderLine
                {
                    MovieID = movieOrderRow.MovieID,
                    Price = movieOrderRow.Price,
                    Quantity = movieOrderRow.Quantity
                };

                orderLines.Add(orderLineForRow);
            }

            return orderLines;
        }

        private void UpdatePricesToOfficialValues(IEnumerable<OrderLine> dataAsOrderLines)
        {
            foreach(OrderLine orderLine in dataAsOrderLines)
            {
                int movieID = orderLine.MovieID;
                Movie associatedMovie = _movieRepository.GetMovieByID(movieID);
                decimal officialMoviePrice = associatedMovie.Price;

                orderLine.Price = officialMoviePrice;
            }
        }

        private IEnumerable<OrderLine> RemoveZeroQuantityOrderLines(IEnumerable<OrderLine> orderLines)
        {
            List<OrderLine> nonZeroQuantityOrderLines = orderLines.ToList();

            foreach (OrderLine orderLine in orderLines)
            {
                if (orderLine.Quantity == 0)
                {
                    nonZeroQuantityOrderLines.Remove(orderLine);
                }
            }

            return nonZeroQuantityOrderLines;
        }

        private decimal TotalOrderLines(IEnumerable<OrderLine> orderLines)
        {
            decimal total = 0.0m;

            foreach (OrderLine orderLine in orderLines)
            {
                total += orderLine.Quantity * orderLine.Price;
            }

            return total;
        }
    }
}