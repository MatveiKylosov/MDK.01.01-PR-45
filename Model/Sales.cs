using System;
using System.ComponentModel.DataAnnotations;

namespace API_Kylosov.Model
{
    /// <summary>
    /// Продажи
    /// </summary>
    public class Sales
    {
        /// <summary>
        /// ID продажи
        /// </summary>
        [Key]
        public int SaleID { get; set; }
        /// <summary>
        /// ID клиента
        /// </summary>
        public int CustomersID { get; set; }
        /// <summary>
        /// ID сотрудника
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// ID автомобиля
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime DateSale { get; set; }
    }
}
