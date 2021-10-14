using MoviesApp.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Client.ApiServices
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie(string id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task DeleteMovie(int id);
    }
}
