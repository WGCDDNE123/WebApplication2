﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{ 
     public class CustomersController : Controller
     { 
        private ApplicationDbContext _context;

          public CustomersController()
          {
               _context = new ApplicationDbContext();
          }
 
          protected override void Dispose(bool disposing)
          {
               _context.Dispose();
          }

          public ViewResult New()
          {
               var membershipTypes = _context.MembershipTypes.ToList();

               var viewModel = new CustomerFormViewModel
               {
                    MembershipTypes = membershipTypes
               };
               return View("CustomerForm", viewModel);  
          }

          [HttpPost]
          public ActionResult Save(Customer customer)
          {
               if (customer.Id == 0)
                    _context.Customers.Add(customer);
               else
               {
                    var customerInDb = _context.Customers.Single(x => x.Id == customer.Id);

                    customerInDb.Name = customer.Name;
                    customerInDb.BirthDate = customer.BirthDate;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                    customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
               }
               _context.SaveChanges();

               return RedirectToAction("Customer", "Customers");

          }

          // GET: Customers
          public ViewResult Customer()//to view the list of customers
          {
             var customers = _context.Customers.Include(c => c.MembershipType); 

             return View(customers);
          } 
 
        public ActionResult Details(int id)
        {
             var customer = _context.Customers.Include(c=> c.MembershipType).SingleOrDefault(c => c.Id == id);

             if (customer == null)
                  return HttpNotFound();

             return View(customer);
        }

        public ActionResult Edit(int id)//snip this one
        {
             var customer = _context.Customers.SingleOrDefault(c=> c.Id == id);

             if(customer == null)
                  return HttpNotFound();

             var viewModel = new CustomerFormViewModel
             {
                  Customer = customer,
                  MembershipTypes = _context.MembershipTypes.ToList()
             };

             return View("CustomerForm", viewModel);
        }
     }
}