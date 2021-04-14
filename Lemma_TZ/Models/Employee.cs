using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lemma_TZ.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Введите имя сотрудника")]
        [StringLength(50, ErrorMessage = "Имя должно быть от {2} до {1} символов", MinimumLength = 2)]
        [Display(Name = "Имя сотрудника")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию сотрудника")]
        [StringLength(50, ErrorMessage = "Фамилия должна быть от {2} до {1} символов", MinimumLength = 2)]
        [Display(Name = "Фамилия сотрудника")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите отчество сотрудника")]
        [StringLength(50, ErrorMessage = "Отчество должно быть от {2} до {1} символов", MinimumLength = 2)]
        [Display(Name = "Отчество сотрудника")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите адрес Email сотрудника")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Введите корректный адрес Email")]
        [Display(Name = "Адрес Email")]
        public string Email { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
