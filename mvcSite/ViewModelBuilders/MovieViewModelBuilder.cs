using mvcSite.IEnumerableExtensions;
using mvcSite.Models;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModels;
using mvcSite.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace mvcSite.ViewModelBuilders
{
    public class MovieViewModelBuilder
    {
        private readonly MovieRepository _movieRepository;

        public MovieViewModelBuilder(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
 
        public MovieViewModel BuildMovieViewModel(int id)
        {
            IEnumerable<Movie> currentMovies = _movieRepository.GetMovies();
            List<Movie> currentMovieList = currentMovies.ToList();

            try
            {
                Movie movieSelected = currentMovieList.Find(movie => movie.ID == id);

                MovieViewModel movieData = new MovieViewModel
                {
                    Name = movieSelected.Name,
                    Description = movieSelected.Description
                };

                return movieData;
            }
            catch
            {
                MovieViewModel errorViewModel = new MovieViewModel
                {
                    Name = "Error",
                    Description = "Movie Not Found"
                };

                return errorViewModel;
            }
        }
    }
}