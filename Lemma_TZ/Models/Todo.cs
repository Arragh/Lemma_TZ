using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lemma_TZ.Models
{
    public class Todo
    {
        public Guid TodoId { get; set; }

        [Required(ErrorMessage = "Введите название задачи")]
        [StringLength(50, ErrorMessage = "Название должно быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Название задачи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите название отдела-заказчика")]
        [StringLength(50, ErrorMessage = "Название должно быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Отдел-заказчик")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Введите название отдела-исполнителя")]
        [StringLength(50, ErrorMessage = "Название должно быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Отдел-исполнитель")]
        public string PerformerName { get; set; }

        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Планируемая дата завершения")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Укажите приоритет задачи")]
        [Display(Name = "Приоритет")]
        [Range(1, 5, ErrorMessage = "Недопустимый диапазон значений")]
        public int Priority { get; set; }
        public bool IsComplete { get; set; } = false;

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
