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
    public class CustomersController : Controller
    {
/*        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Customers>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Customers> customers = new CustomersContext().Customers;
                return Json(customers);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }*/

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
    }
}
