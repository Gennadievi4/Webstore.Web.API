using WebStore.Domain.Entitys.Base;

namespace WebStore.Domain.Entitys
{
    /// <summary>Сотрудник/// </summary>
    public class Employee : Entity
    {
        /// <summary>Имя/// </summary>
        public string FirstName { get; set; }

        /// <summary>Фамилия/// </summary>
        public string LastName { get; set; }

        /// <summary>Отчество/// </summary>
        public string Patronymic { get; set; }

        /// <summary>Возраст/// </summary>
        public int Age { get; set; }
    }
}
