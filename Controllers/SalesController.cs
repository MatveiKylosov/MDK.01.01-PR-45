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
        /// <summary>
        /// Получение продажи по ID
        /// </summary>
        /// <param name="saleID">ID продажи</param>
        /// <returns>Данный метод предназначен для получения продажи по ID</returns>
        /// <response code = "200">Продажа успешно получена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Получение списка продаж
        /// </summary>
        /// <param name="sortBy">Параметр сортировки</param>
        /// <returns>Данный метод предназначен для получения списка продаж</returns>
        /// <response code = "200">Список продаж успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Добавление новой продажи
        /// </summary>
        /// <param name="sale">Объект продажи</param>
        /// <returns>Данный метод предназначен для добавления новой продажи</returns>
        /// <response code = "200">Продажа успешно добавлена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Обновление продажи
        /// </summary>
        /// <param name="sale">Объект продажи</param>
        /// <returns>Данный метод предназначен для обновления продажи</returns>
        /// <response code = "200">Продажа успешно обновлена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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
