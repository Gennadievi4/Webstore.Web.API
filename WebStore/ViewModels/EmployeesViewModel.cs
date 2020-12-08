using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EmployeesViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>Имя/// </summary>
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя - обязательное поле для заполнения")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Длинна имени может быть от 1 до 30!")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)")]
        public string FirstName { get; set; }

        /// <summary>Фамилия/// </summary>
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия - обязательное поле для заполнения")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Длинна фамилии может быть от 1 до 30!")]
        public string LastName { get; set; }

        /// <summary>Отчество/// </summary>
        [Display(Name = "Отчество")]
        [StringLength(30)]
        public string Patronymic { get; set; }

        /// <summary>Возраст/// </summary>
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}
