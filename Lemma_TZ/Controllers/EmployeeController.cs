using Lemma_TZ.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lemma_TZ.Controllers
{
    public class EmployeeController : Controller
    {
        private IDataRepository repository;
        public EmployeeController(IDataRepository repository) => this.repository = repository;

        public IActionResult Index() => View(repository.Employees);

        // Добавления нового пользователя [GET]
        [HttpGet]
        public IActionResult AddEmployee() => View();

        // Добавления нового пользователя [POST]
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Редактирование пользователя [GET]
        [HttpGet]
        public IActionResult EditEmployee(Guid employeeId) => View(repository.GetEmployee(employeeId));

        // Редактирование пользователя [POST]
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            // Возврат с ошибками валидации
            return View(employee);
        }

        // Удаление пользователя
        [HttpPost]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            repository.DeleteEmployee(employeeId);
            return RedirectToAction("Index");
        }
    }
}
