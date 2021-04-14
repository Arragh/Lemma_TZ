using Lemma_TZ.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lemma_TZ.Controllers
{
    public class MainController : Controller
    {
        private IDataRepository repository;
        public MainController(IDataRepository repository) => this.repository = repository;

        // Главная страница
        public IActionResult Index(DateTime startDate, DateTime endDate, int minPriority = 1, int maxPriority = 5, bool onlyUnComplete = false, string orderValue = null)
        {
            // Список задач, отфильтрованный по заданным параметрам
            var tasks = repository.GetFilteredTasks(startDate, endDate, minPriority, maxPriority, onlyUnComplete, orderValue);

            // Вьюбаги для datetime-инпута на странице (чтобы дата в инпуте не сбрасывалась)
            if (startDate != DateTime.MinValue)
            {
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            }
            if (endDate != DateTime.MinValue)
            {
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            }

            // Вьюбаги для остальных инпутов
            ViewBag.MinPriority = minPriority;
            ViewBag.MaxPriority = maxPriority;
            ViewBag.OnlyUnComplete = onlyUnComplete;
            ViewBag.OrderValue = orderValue;

            return View(tasks);
        }
    }
}
