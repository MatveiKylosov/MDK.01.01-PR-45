using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API_Kylosov.Controllers
{
    [Route("api/CarBrandsController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CarBrandsController : Controller
    {
        /// <summary>
        /// Получение информации о бренде автомобиля по названию
        /// </summary>
        /// <param name="brandName">Название бренда</param>
        /// <returns>Данный метод предназначен для получения информации о бренде автомобиля по названию</returns>
        /// <response code = "200">Информация о бренде успешно получена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(CarBrands), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(string brandName)
        {
            try
            {
                CarBrands carBrand = new CarBrandsContext().CarBrands.Where(x => x.BrandName == brandName).First();
                return Json(carBrand);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        /// <summary>
        /// Получение списка брендов автомобилей
        /// </summary>
        /// <param name="sortBy">Параметр сортировки</param>
        /// <returns>Данный метод предназначен для получения списка брендов автомобилей</returns>
        /// <response code = "200">Список брендов успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CarBrands>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List(string sortBy = "BrandName")
        {
            try
            {
                IEnumerable<CarBrands> carBrands = new CarBrandsContext().CarBrands.OrderBy(x => EF.Property<object>(x, sortBy));
                return Json(carBrands);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        /// <summary>
        /// Добавление нового бренда автомобиля
        /// </summary>
        /// <param name="CarBrands">Данные о бренде автомобиля</param>
        /// <returns>Данный метод предназначен для добавления нового бренда автомобиля</returns>
        /// <response code = "200">Бренд автомобиля успешно добавлен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] CarBrands CarBrands)
        {
            try
            {
                CarBrandsContext CarBrandsContext = new CarBrandsContext();
                CarBrandsContext.CarBrands.Add(CarBrands);
                CarBrandsContext.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        /// <summary>
        /// Обновление информации о бренде автомобиля
        /// </summary>
        /// <param name="CarBrands">Обновленные данные о бренде автомобиля</param>
        /// <returns>Данный метод предназначен для обновления информации о бренде автомобиля</returns>
        /// <response code = "200">Информация о бренде автомобиля успешно обновлена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] CarBrands CarBrands)
        {
            try
            {
                CarBrandsContext CarBrandsContext = new CarBrandsContext();
                var existingCarBrand = CarBrandsContext.CarBrands.Find(CarBrands.BrandName);
                if (existingCarBrand != null)
                {
                    existingCarBrand.CountryOrigin = CarBrands.CountryOrigin;
                    existingCarBrand.ManufacturerFactory = CarBrands.ManufacturerFactory;
                    existingCarBrand.Address = CarBrands.Address;
                    CarBrandsContext.SaveChanges();

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