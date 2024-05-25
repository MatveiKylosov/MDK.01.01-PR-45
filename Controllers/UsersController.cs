using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API_Kylosov.Controllers
{
    [Route("api/UsersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для авторизации пользователя на сайте</returns>
        /// /// <response code = "200">Пользователь успешно авторизован</response>
        /// /// <response code = "403">Ошибка запроса, данные не указан</response>
        /// /// <response code = "200">При выполнении запроса возникли ошибки</response>
        [Route("SingIn")]
        [HttpPost]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult SingIn([FromForm] string Login, [FromForm] string Password)
        {
            if(Login == null || Password == null)
                return StatusCode(403);
            try
            {
                StringBuilder builder = new StringBuilder();
                using (SHA384 sha384Hash = SHA384.Create())
                {
                    byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    for (int i = 0; i < bytes.Length; i++)
                        builder.Append(bytes[i].ToString("x2"));
                }

                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == builder.ToString()).FirstOrDefault();
                if (User == null)
                {
                    return StatusCode(401);
                }
                return Json(User);
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для регистрации пользователя на сайте</returns>
        /// /// <response code = "200">Пользователь успешно зарегистрирована</response>
        /// /// <response code = "403">Ошибка запроса, данные не указан</response>
        /// /// <response code = "200">При выполнении запроса возникли ошибки</response>

        [Route("RegIn")]
        [HttpPost]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password, [FromForm] string Token)
        {
            if (Login == null || Password == null || Token == null)
                return StatusCode(403);
            try
            {
                Users User = new Users();
                User.Login = Login;

                if(new UsersContext().Users.Where(x => x.Token == Token).FirstOrDefault() == null)
                {
                    return StatusCode(403);
                }

                StringBuilder builder = new StringBuilder();
                using (SHA384 sha384Hash = SHA384.Create())
                {
                    byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    for (int i = 0; i < bytes.Length; i++)
                        builder.Append(bytes[i].ToString("x2"));
                }

                User.Password = builder.ToString();

                UsersContext usersContext = new UsersContext();
                User.Token = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 16).Select(s => s[new Random().Next(s.Length)]).ToArray());
                usersContext.Users.Add(User);
                usersContext.SaveChanges();

                return Json(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Удаление пользователя по ID
        /// </summary>
        /// <param name="id">ID пользователя для удаления</param>
        /// <returns>Результат операции удаления</returns>
        /// <response code="200">Пользователь успешно удален</response>
        /// <response code="1337">Неправильный токен</response>
        /// <response code="404">Пользователь с указанным ID не найден</response>
        /// <response code="500">Ошибка сервера при выполнении операции удаления</response>
        [Route("Delete/{id}")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(1337)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteUser(int id, [FromForm] string Token)
        {
            try
            {
                UsersContext usersContext = new UsersContext();
                var userToDelete = usersContext.Users.Find(id);
                if (userToDelete == null)
                    return NotFound();

                if (userToDelete.Token != Token)
                    return StatusCode(1337);

                usersContext.Users.Remove(userToDelete);
                usersContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Полная очистка таблицы пользователей
        /// </summary>
        /// <returns>Результат операции очистки</returns>
        /// <response code="200">Таблица успешно очищена</response>
        /// <response code="500">Ошибка сервера при выполнении операции очистки</response>
        [Route("Clear")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult ClearUsers()
        {
            try
            {
                UsersContext usersContext = new UsersContext();
                usersContext.Users.RemoveRange(usersContext.Users);
                usersContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
