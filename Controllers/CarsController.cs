using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_Kylosov.Controllers
{
    [Route("api/CarsController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CarsController : Controller
    {
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


        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Cars>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List(string sortBy = "Name")
        {
            try
            {
                IEnumerable<Cars> cars = new CarsContext().Cars.OrderBy(x => EF.Property<object>(x, sortBy));
                return Json(cars);
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
        public ActionResult Add([FromForm] Cars car)
        {
            try
            {
                CarsContext carsContext = new CarsContext();
                carsContext.Cars.Add(car);
                carsContext.SaveChanges();

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
        public ActionResult Update([FromForm] Cars car)
        {
            try
            {
                CarsContext carsContext = new CarsContext();
                var existingCar = carsContext.Cars.Find(car.CarID);
                if (existingCar != null)
                {
                    existingCar.Name = car.Name;
                    existingCar.Stamp = car.Stamp;
                    existingCar.YearProduction= car.YearProduction;
                    existingCar.Colour = car.Colour;
                    existingCar.Category = car.Category;
                    existingCar.Price = car.Price;

                    carsContext.SaveChanges();

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
