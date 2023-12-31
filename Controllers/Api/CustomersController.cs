﻿using System;
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
    public class CustomersController : ApiController
    {
         private ApplicationDbContext _context;

         public CustomersController()
         {
              _context = new ApplicationDbContext();
         }

         // GET /api/customers
         public IEnumerable<CustomerDto> GetCustomers()
         {
              return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
         }

         // GET /api/customers/1
         public IHttpActionResult GetCustomer(int id)
         {
              var customer = _context.Customers.SingleOrDefault(c=>c.Id == id);

              if (customer == null)
                   return NotFound();

              return Ok(Mapper.Map<Customer, CustomerDto>(customer));
         }

         //POST /api/customer (to post a customer to customers collection)
         [HttpPost]
         public IHttpActionResult CreateCustomer(CustomerDto customerDto)
         {
              if (!ModelState.IsValid)
                   return BadRequest();

              var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
              _context.Customers.Add(customer);
              _context.SaveChanges();
 
              customerDto.Id = customer.Id;
              return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
              //getting the uri of our current request with a slash beside it 
         }

         // PUT /api/customers/1
         [HttpPut]
         public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
         {
              if (!ModelState.IsValid)
                   return BadRequest();
              var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

              if (customerInDb == null)
                   return NotFound();
              Mapper.Map(customerDto, customerInDb);
              
              _context.SaveChanges();
              return Ok();    
         }

         //DELETE /api/customers/1
         [HttpDelete]
         public IHttpActionResult DeleteCustomer(int id)
         {
              var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

              if (customerInDb == null)
                   return NotFound();
              _context.Customers.Remove(customerInDb);
              _context.SaveChanges();

              return Ok();
         }
    }                                                                                                                              
}
