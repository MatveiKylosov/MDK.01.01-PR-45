using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace API_Kylosov.Controllers
{
    [Route("api/CarsController")]
    public class CarsController : Controller
    {
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Cars>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Cars> cars = new CarsContext().Cars;
                return Json(cars);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Cars), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int carID)
        {
            try
            {
                Cars car = new CarsContext().Cars.Where(x => x.CarID == carID).First();
                return Json(car);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
