using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mvcSite.DAL
{
    public class MovieDatabaseReader : IAllMoviesOnlyReader
    {
        public IEnumerable<Movie> Movies;

        public IEnumerable<Movie> GetMovies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            using SqlConnection dbConnection = new SqlConnection(connStr);
            using (dbConnection)
            {
                dbConnection.Open();
                IEnumerable<Movie> currentMovies = ReadMoviesFromDB(dbConnection);

                return currentMovies;
            }
        }

        private List<Movie> ReadMoviesFromDB(SqlConnection conn)
        {
            SqlCommand readCommand = new SqlCommand("ReadMovie", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (readCommand)
            {
                List<Movie> currentMovies = new List<Movie>();

                SqlDataReader reader = readCommand.ExecuteReader();

                while (reader.Read())
                {
                    Movie movieReadFromDatabase = new Movie
                    {
                        ID = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Price = decimal.Parse(reader["Price"].ToString()),
                        Description = reader["Description"].ToString()
                    };

                    currentMovies.Add(movieReadFromDatabase);
                }

                return currentMovies;
            }
        }
    }
}
