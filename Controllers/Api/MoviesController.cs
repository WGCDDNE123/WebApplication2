using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers.Api
{
    public class MoviesController : ApiController
    {
         private ApplicationDbContext _dbContext;

         public MoviesController()
         {
              _dbContext = new ApplicationDbContext();
         }

         public IEnumerable<MovieDto> GetMovies()
         {
              return _dbContext.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
         }

         // GET /api/movies/1
         public IHttpActionResult GetMovieById(int id)
         {
              var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

              if (movie == null)
                   return NotFound();

              return Ok(Mapper.Map<Movie, MovieDto>(movie));
         }

         // POST /api/movies
         [HttpPost]
         public IHttpActionResult CreateMovie(MovieDto movieDto)
         {
              if (!ModelState.IsValid)
                   return BadRequest();

              var movie = Mapper.Map<MovieDto, Movie>(movieDto);
              _dbContext.Movies.Add(movie);
              _dbContext.SaveChanges();

              movieDto.Id = movie.Id;
              return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
         }

         // PUT /api/movies/1
         [HttpPut]
         public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
         {
              if (!ModelState.IsValid)//if not valid....
                    return BadRequest();

              var movieInDb = _dbContext.Movies.SingleOrDefault(m=>m.Id == id);

              if (movieInDb == null)
                   return NotFound();

              Mapper.Map(movieDto, movieInDb);

              _dbContext.SaveChanges();

              return Ok();
         }

         //DELETE /api/movies/1
         [HttpDelete]
         public IHttpActionResult DeleteMovie(int id)
         {
              var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

              if (movieInDb == null)
                   return NotFound();
              _dbContext.Movies.Remove(movieInDb);
              _dbContext.SaveChanges();

              return Ok();
         }
    }
}
