using mvcSite.Models;
using mvcSite.Repositories;
using mvcSite.ViewModels.Movie;
using System.Collections.Generic;
using System.Linq;

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
            Movie movieSelected = _movieRepository.GetMovieByID(id);//better to use the GetMovie() method here

            if (movieSelected == null)
            {
                return null;
            }

            MovieViewModel movieData = new MovieViewModel
            {
                Name = movieSelected.Name,
                Description = movieSelected.Description
            };

            return movieData;
        }
    }
}