using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Kylosov.Controllers
{
    [Route("api/TasksController")]
    public class TaskContoller : Controller
    {
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Tasks> Tasks = new TasksContext().Tasks;
                return Json(Tasks);
            }
            catch(Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Tasks Tasks = new TasksContext().Tasks.Where(x=> x.Id == Id).First();
                return Json(Tasks);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
