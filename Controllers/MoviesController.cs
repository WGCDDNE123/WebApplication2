using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

         protected override void Dispose(bool disposing)
         {
             _context.Dispose();
         }

         // GET: Movies/Random
        public ViewResult Random()
        {
             var movies = _context.Movies.Include(m=>m.Genre).ToList(); 
             return View(movies);
        }

        public ActionResult Details(int id)
        {
             var movie = _context.Movies.Include(m=>m.Genre).SingleOrDefault(m => m.Id == id);

             if (movie == null)
                  return HttpNotFound();
             return View(movie);
        }
    }

}