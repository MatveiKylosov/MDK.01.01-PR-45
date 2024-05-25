using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Kylosov.Controllers
{
    [Route("api/TasksController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TaskContoller : Controller
    {
        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <returns>Данный метод предназначен для получения списка задач</returns>
        /// <response code = "200">Список задач успешно получен</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Получение задачи по ID
        /// </summary>
        /// <param name="Id">ID задачи</param>
        /// <returns>Данный метод предназначен для получения задачи по ID</returns>
        /// <response code = "200">Задача успешно получена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
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

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task">Объект задачи</param>
        /// <returns>Данный метод предназначен для добавления новой задачи</returns>
        /// <response code = "200">Задача успешно добавлена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm]Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                tasksContext.Tasks.Add(task);
                tasksContext.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="task">Объект задачи</param>
        /// <returns>Данный метод предназначен для обновления задачи</returns>
        /// <response code = "200">Задача успешно обновлена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                var existingTask = tasksContext.Tasks.Find(task.Id);
                if (existingTask != null)
                {
                    existingTask.Name = task.Name;
                    existingTask.Priority = task.Priority;
                    existingTask.DateExecute = task.DateExecute;
                    existingTask.Comment = task.Comment;
                    existingTask.Done = task.Done;

                    tasksContext.SaveChanges();

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

        /// <summary>
        /// Удаление задачи по ID
        /// </summary>
        /// <param name="id">ID задачи</param>
        /// <returns>Данный метод предназначен для удаления задачи по ID</returns>
        /// <response code = "200">Задача успешно удалена</response>
        /// <response code = "404">Задача не найдена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Delete/{id}")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Delete(int id)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                var existingTask = tasksContext.Tasks.Find(id);
                if (existingTask != null)
                {
                    tasksContext.Tasks.Remove(existingTask);
                    tasksContext.SaveChanges();

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

        /// <summary>
        /// Полная очистка таблицы задач
        /// </summary>
        /// <returns>Данный метод предназначен для полной очистки таблицы задач</returns>
        /// <response code = "200">Таблица задач успешно очищена</response>
        /// <response code = "500">При выполнении запроса возникли ошибки</response>
        [Route("Clear")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Clear()
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                tasksContext.Tasks.RemoveRange(tasksContext.Tasks);
                tasksContext.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
