using System.ComponentModel.DataAnnotations;

namespace API_Kylosov.Model
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    public class Employees
    {
        /// <summary>
        /// ID сотрудника
        /// </summary>
        [Key]
        public int EmployeeID { get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Опыт работы
        /// </summary>
        public int Experience { get; set; }
        /// <summary>
        /// Зарплата
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
