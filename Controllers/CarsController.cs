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
        /// <summary>
        /// Получение автомобиля по ID
        /// </summary>
        /// <param name="carID">ID автомобиля</param>
        /// <returns>Данный метод предназначен для получения автомобиля по ID</returns>
        /// <response code = "200">Автомобиль успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Получение списка автомобилей
        /// </summary>
        /// <param name="sortBy">Параметр сортировки</param>
        /// <returns>Данный метод предназначен для получения списка автомобилей</returns>
        /// <response code = "200">Список автомобилей успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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


        /// <summary>
        /// Добавление нового автомобиля
        /// </summary>
        /// <param name="car">Объект автомобиля</param>
        /// <returns>Данный метод предназначен для добавления нового автомобиля</returns>
        /// <response code = "200">Автомобиль успешно добавлен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Обновление автомобиля
        /// </summary>
        /// <param name="car">Объект автомобиля</param>
        /// <returns>Данный метод предназначен для обновления автомобиля</returns>
        /// <response code = "200">Автомобиль успешно обновлен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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
