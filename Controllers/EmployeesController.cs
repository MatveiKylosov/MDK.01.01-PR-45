using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace API_Kylosov.Controllers
{
    [Route("api/EmployeesController")]
    public class EmployeesController : Controller
    {
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Employees>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Employees> employees = new EmployeesContext().Employees;
                return Json(employees);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Employees), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int employeeID)
        {
            try
            {
                Employees employee = new EmployeesContext().Employees.Where(x => x.EmployeeID == employeeID).First();
                return Json(employee);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
