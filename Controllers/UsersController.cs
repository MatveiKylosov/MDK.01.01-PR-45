using API_Kylosov.Context;
using API_Kylosov.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == Password).FirstOrDefault();
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
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);
            try
            {
                Users User = new Users();
                User.Login = Login;
                User.Password = Password;
                UsersContext usersContext = new UsersContext();
                usersContext.Users.Add(User);
                usersContext.SaveChanges();
                return Json(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
