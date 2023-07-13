using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

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

         // GET: Movies/Index
        public ViewResult Index ()
        {
             var movies = _context.Movies.Include(m=>m.Genre).ToList(); 
             return View(movies);
        }

        public ActionResult Details(int id)
        {
             var detail = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

             if (detail == null)
                  return HttpNotFound();
             return View(detail);
        }

        public ViewResult New()
        {
             var genres = _context.Genres.ToList();

             var viewMod = new MovieFormViewModel
             {
                  Genres = genres
             };
             return View("MovieForm", viewMod);
        }

        public ActionResult Save(Movie movie)
        {
             if (movie.Id == 0)
             {
                  movie.DateAdded = DateTime.Now;
                  _context.Movies.Add(movie);
             }
             else
             {
                  var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                  movieInDb.Name = movie.Name;
                  movieInDb.ReleaseDate = movie.ReleaseDate;
                  movieInDb.GenreId = movie.GenreId;
                  movieInDb.NumberInStock = movie.NumberInStock;
             }
             
             _context.SaveChanges();//remaining to add how to update data before then saving

               return RedirectToAction("Index", "Movies");//remember actionName then controller name
          } 

        public ActionResult EditMovie(int id)
        {
             var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

             if(movie == null)
                  return HttpNotFound();

             var viewModel = new MovieFormViewModel
             {
                  Movie = movie,
                  Genres = _context.Genres.ToList()
             };
             return View("MovieForm", viewModel);
        }
    }

}