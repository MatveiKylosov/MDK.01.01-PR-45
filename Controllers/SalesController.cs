using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace API_Kylosov.Controllers
{
    [Route("api/SalesController")]
    public class SalesController : Controller
    {
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Sales>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Sales> sales = new SalesContext().Sales;
                return Json(sales);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

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
    }
}
