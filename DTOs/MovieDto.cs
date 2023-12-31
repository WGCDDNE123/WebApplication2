﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.DTOs
{
     public class MovieDto
     {
          public int Id { get; set; }
          [Required]
          [StringLength(255)]
          public string Name { get; set; }
          [Required]
          public int GenreId { get; set; }
          public DateTime DateAdded { get; set; }
          public DateTime ReleaseDate { get; set; }

          [Range(1, 20)]
          public byte NumberInStock { get; set; }
     }
}