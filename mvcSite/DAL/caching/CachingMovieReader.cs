using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcSite.Models;

namespace mvcSite.DAL.caching
{
    public class CachingMovieReader : IMovieReader
    {
        private readonly IAllMoviesOnlyReader _allMoviesReader;
        private readonly Lazy<IEnumerable<Movie>> _movies;

        public CachingMovieReader(IAllMoviesOnlyReader allMoviesReader)
        {
            _allMoviesReader = allMoviesReader;
            _movies = new Lazy<IEnumerable<Movie>>(() =>
            {
                return _allMoviesReader.GetMovies();
            });
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movies.Value;
        }

        public Movie GetMovieById(int id)
        {
            List<Movie> movieList = GetMovies().ToList();
            Movie movieWithMatchingID = movieList.Find(movie => movie.ID == id);
            return movieWithMatchingID;
        }
    }
}