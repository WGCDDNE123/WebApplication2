using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MoviesController : Controller
    {
         private ApplicationDbContext _context;

         public MoviesController()
         {
              _context = new ApplicationDbContext();
         }
        // GET: Movies/Random
        public ViewResult Random ()
        {
             var movies = GetMovies(); 
             return View(movies);
        }

        private IEnumerable<Movie> GetMovies()
        {
             return new List<Movie>
             {
                  new Movie { Id = 1, Name = "Shrek" },
                  new Movie { Id = 2, Name = "Prison Break" }
             };
        }






    }

}