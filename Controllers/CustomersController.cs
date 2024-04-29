using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_Kylosov.Controllers
{
    [Route("api/CustomersController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CustomersController : Controller
    {
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Customers), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int customersID)
        {
            try
            {
                Customers customer = new CustomersContext().Customers.Where(x => x.CustomersID == customersID).First();
                return Json(customer);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Customers>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List(string sortBy = "FullName")
        {
            try
            {
                IEnumerable<Customers> customers = new CustomersContext().Customers.OrderBy(x => EF.Property<object>(x, sortBy));
                return Json(customers);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] Customers customer)
        {
            try
            {
                CustomersContext customersContext = new CustomersContext();
                customersContext.Customers.Add(customer);
                customersContext.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Customers customer)
        {
            try
            {
                CustomersContext customersContext = new CustomersContext();
                var existingCustomer = customersContext.Customers.Find(customer.CustomersID);
                if (existingCustomer != null)
                {
                    existingCustomer.FullName = customer.FullName;
                    existingCustomer.PassportDetails = customer.PassportDetails;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.City = customer.City;
                    existingCustomer.DateOfBirth = customer.DateOfBirth;
                    existingCustomer.Gender = customer.Gender;
                    existingCustomer.Password = customer.Password;

                    customersContext.SaveChanges();

                    return StatusCode(200);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
