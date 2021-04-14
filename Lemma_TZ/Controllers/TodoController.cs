using Lemma_TZ.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Lemma_TZ.Controllers
{
    public class TodoController : Controller
    {
        private IDataRepository repository;
        public TodoController(IDataRepository repository) => this.repository = repository;

        #region Создать задачу
        // Создание новой задачи [GET]
        [HttpGet]
        public IActionResult AddTask() => View(new Todo { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date });

        // Создание новой задачи [POST]
        [HttpPost]
        public IActionResult AddTask(Todo todo)
        {
            // Дата начала задачи не должна быть позже даты ее предполагаемого завершения
            if (todo.StartDate >= todo.EndDate)
            {
                ModelState.AddModelError("EndDate", "Дата окончания должна быть после даты начала");
            }

            if (ModelState.IsValid)
            {
                repository.AddTask(todo);
                return RedirectToAction("Index", "Main");
            }

            // Возврат с ошибками валидации
            return View(todo);
        }
        #endregion

        #region Изменить задачу
        // Редактирование задачи [GET]
        [HttpGet]
        public IActionResult EditTask(Guid todoId, Guid addEmployeeId, Guid removeEmployeeId)
        {
            // Проверка на дефолтное значение (aka null)
            if (addEmployeeId != default(Guid))
            {
                repository.AddEmployeeToTask(todoId, addEmployeeId);
            }
            if (removeEmployeeId != default(Guid))
            {
                repository.RemoveEmployeeFromTask(todoId, removeEmployeeId);
            }

            Todo todo = repository.GetTask(todoId);

            // Список работников, не задействованный в работе над задачей
            ViewBag.Employees = repository.Employees.ToList().Except(todo.Employees);

            return View(todo);
        }

        // Редактирование задачи [POST]
        [HttpPost]
        public IActionResult EditTask(Todo todo)
        {
            // Дата начала задачи не должна быть позже даты ее предполагаемого завершения
            if (todo.StartDate > todo.EndDate)
            {
                ModelState.AddModelError("EndDate", "Дата окончания должна быть после даты начала");
            }

            // Если задача будет иметь статус "Завершена", ее редактирование будет невозможно
            if (ModelState.IsValid && !todo.IsComplete)
            {
                repository.UpdateTask(todo);
                return RedirectToAction("Index", "Main");
            }

            // Возврат с ошибками
            return View(todo);
        }

        // Пометить задачу как завершенную
        [HttpGet]
        public IActionResult CompleteTask(Guid todoId)
        {
            repository.CompleteTask(todoId);
            return RedirectToAction("Index", "Main");
        }
        #endregion

        #region Удалить задачу
        [HttpPost]
        public IActionResult DeleteTask(Guid todoId)
        {
            repository.DeleteTask(todoId);
            return RedirectToAction("Index", "Main");
        }
        #endregion
    }
}
