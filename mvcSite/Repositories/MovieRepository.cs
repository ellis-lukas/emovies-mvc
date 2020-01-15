using mvcSite.DAL;
using mvcSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace mvcSite.Repositories
{
    public class MovieRepository
    {
        private readonly IMovieReader _movieReader;

        public MovieRepository(IMovieReader movieReader)
        {
            _movieReader = movieReader;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieReader.GetMovies();
        }

        public Movie GetMovieByID(int id)
        {
            return _movieReader.GetMovieById(id);
        }
    }
}
