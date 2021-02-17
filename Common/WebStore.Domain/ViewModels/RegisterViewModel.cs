﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree", "Login")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmed { get; set; }
    }
}
