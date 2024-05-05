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
        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <param name="customersID">ID клиента</param>
        /// <returns>Данный метод предназначен для получения клиента по ID</returns>
        /// <response code = "200">Клиент успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Получение списка клиентов
        /// </summary>
        /// <param name="sortBy">Параметр сортировки</param>
        /// <returns>Данный метод предназначен для получения списка клиентов</returns>
        /// <response code = "200">Список клиентов успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="customer">Объект клиента</param>
        /// <returns>Данный метод предназначен для добавления нового клиента</returns>
        /// <response code = "200">Клиент успешно добавлен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Обновление клиента
        /// </summary>
        /// <param name="customer">Объект клиента</param>
        /// <returns>Данный метод предназначен для обновления клиента</returns>
        /// <response code = "200">Клиент успешно обновлен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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
