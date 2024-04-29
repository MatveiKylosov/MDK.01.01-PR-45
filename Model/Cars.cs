namespace API_Kylosov.Model
{
    /// <summary>
    /// Автомобили
    /// </summary>
    public class Cars
    {
        /// <summary>
        /// ID автомобиля
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Марка
        /// </summary>
        public string Stamp { get; set; }
        /// <summary>
        /// Год производства
        /// </summary>
        public int YearProduction { get; set; }
        /// <summary>
        /// Цвет
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
