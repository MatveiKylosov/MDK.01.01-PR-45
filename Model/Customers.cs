using System;
using System.ComponentModel.DataAnnotations;

namespace API_Kylosov.Model
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public class Customers
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        [Key]
        public int CustomersID { get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Паспортные данные
        /// </summary>
        public string PassportDetails { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }

}
