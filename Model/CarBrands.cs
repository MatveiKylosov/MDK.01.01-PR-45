namespace API_Kylosov.Model
{
    /// <summary>
    /// Марки автомобилей
    /// </summary>
    public class CarBrands
    {
        /// <summary>
        /// Название бренда
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// Страна происхождения
        /// </summary>
        public string CountryOrigin { get; set; }
        /// <summary>
        /// Завод изготовитель
        /// </summary>
        public string ManufacturerFactory { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
    }
}
