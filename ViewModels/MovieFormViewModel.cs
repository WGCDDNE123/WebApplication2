﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
     public class MovieFormViewModel
     {
          public IEnumerable<Genre> Genres { get; set; }

          public int? Id { get; set; }
          [Required]
          [StringLength(255)]
          public string Name { get; set; }

          [Required]
          [Display(Name = "Genre")]
          public int? GenreId { get; set; }

          [Display(Name = "Release Date")]
          [Required]
          public DateTime? ReleaseDate { get; set; }

          [Display(Name = "Number In Stock")]
          [Required]
          [Range(1,20)]
          public byte? NumberInStock { get; set; }

          public string Title => Id != 0 ? "Edit Movie" : "New Movie";

          public MovieFormViewModel()
          {
               Id = 0;
          }

          public MovieFormViewModel(Movie movie)
          {
               Id = movie.Id;
               Name = movie.Name;
               ReleaseDate = movie.ReleaseDate;
               GenreId = movie.GenreId;
               NumberInStock = movie.NumberInStock;
          }
     }
}