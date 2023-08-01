using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.App_Start
{
     public class MappingProfile: Profile
     {
          public MappingProfile()
          {
               //Domain to Dto
               Mapper.CreateMap<Customer, CustomerDto>();
               Mapper.CreateMap<Movie, MovieDto>();

               //Dto to Domain
               Mapper.CreateMap<CustomerDto, Customer>()
                    .ForMember(c=>c.Id, opt => opt.Ignore());
               Mapper.CreateMap<MovieDto, Movie>()
                    .ForMember(c => c.Id, opt => opt.Ignore());
          }
     }
}