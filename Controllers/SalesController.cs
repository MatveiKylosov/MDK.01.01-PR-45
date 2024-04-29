using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_Kylosov.Controllers
{
    [Route("api/SalesController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SalesController : Controller
    {
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Sales), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int saleID)
        {
            try
            {
                Sales sale = new SalesContext().Sales.Where(x => x.SaleID == saleID).First();
                return Json(sale);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Sales>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List(string sortBy = "DateSale")
        {
            try
            {
                IEnumerable<Sales> sales = new SalesContext().Sales.OrderBy(x => EF.Property<object>(x, sortBy));
                return Json(sales);
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
        public ActionResult Add([FromForm] Sales sale)
        {
            try
            {
                SalesContext salesContext = new SalesContext();
                salesContext.Sales.Add(sale);
                salesContext.SaveChanges();

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
        public ActionResult Update([FromForm] Sales sale)
        {
            try
            {
                SalesContext salesContext = new SalesContext();
                var existingSale = salesContext.Sales.Find(sale.SaleID);
                if (existingSale != null)
                {
                    existingSale.CustomersID = sale.CustomersID;
                    existingSale.EmployeeID = sale.EmployeeID;
                    existingSale.CarID = sale.CarID;
                    existingSale.DateSale = sale.DateSale;

                    salesContext.SaveChanges();

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
