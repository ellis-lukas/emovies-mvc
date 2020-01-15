using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcSite.DAL
{
    public interface IAllMoviesOnlyReader
    {
        IEnumerable<Movie> GetMovies();
    }
}
