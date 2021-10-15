namespace MoviesApp.API.Data
{
    public class MoviesAppAPIContext : DbContext
    {
        public MoviesAppAPIContext (DbContextOptions<MoviesAppAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
