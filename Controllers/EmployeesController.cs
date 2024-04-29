using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace API_Kylosov.Controllers
{
    [Route("api/EmployeesController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class EmployeesController : Controller
    {

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

        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Employees>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List(string sortBy = "FullName")
        {
            try
            {
                IEnumerable<Employees> employees = new EmployeesContext().Employees.OrderBy(x => EF.Property<object>(x, sortBy));
                return Json(employees);
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
        public ActionResult Add([FromForm] Employees employee)
        {
            try
            {
                EmployeesContext employeesContext = new EmployeesContext();
                employeesContext.Employees.Add(employee);
                employeesContext.SaveChanges();

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
        public ActionResult Update([FromForm] Employees employee)
        {
            try
            {
                EmployeesContext employeesContext = new EmployeesContext();
                var existingEmployee = employeesContext.Employees.Find(employee.EmployeeID);
                if (existingEmployee != null)
                {
                    existingEmployee.FullName = employee.FullName;
                    existingEmployee.Experience = employee.Experience;
                    existingEmployee.Salary = employee.Salary;
                    existingEmployee.Password = employee.Password;

                    employeesContext.SaveChanges();

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
