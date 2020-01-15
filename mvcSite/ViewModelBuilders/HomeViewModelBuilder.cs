using mvcSite.IEnumerableExtensions;
using mvcSite.Models;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModels.Home;
using System.Collections.Generic;

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

        public List<MovieOrderRow> BuildMovieOrderRowsWithZeroAsQuantityOrdered()
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

        public void SaveNonZeroOrderLinesFromHomeViewModel(HomeViewModel movieOrderData)
        {
            IEnumerable<OrderLine> dataAsOrderLines = ConvertHomeViewModelToOrderLines(movieOrderData);
            IEnumerable<OrderLine> nonZeroOrderLines = dataAsOrderLines.RemoveZeroQuantityOrderLines();
            _sessionManager.NonZeroOrderLines = nonZeroOrderLines;
        }

        public IEnumerable<OrderLine> ConvertHomeViewModelToOrderLines(HomeViewModel homeViewModel)
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
    }
}