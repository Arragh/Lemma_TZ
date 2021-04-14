using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemma_TZ.Models
{
    public class EFDataRepository : IDataRepository
    {
        private DataDbContext context;
        public EFDataRepository(DataDbContext context) => this.context = context;

        public IQueryable<Todo> Todos => context.Todos;
        public IQueryable<Employee> Employees => context.Employees;

        #region Tasks
        public void AddTask(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
        }

        public void CompleteTask(Guid todoId)
        {
            Todo todo = context.Todos.Find(todoId);
            todo.IsComplete = true;
            context.SaveChanges();
        }

        public void DeleteTask(Guid todoId)
        {
            context.Todos.Remove(new Todo { TodoId = todoId });
            context.SaveChanges();
        }

        public Todo GetTask(Guid todoId) => context.Todos.Include(t => t.Employees).First(t => t.TodoId == todoId);
        public IEnumerable<Todo> GetFilteredTasks(DateTime startDate, DateTime endDate, int minPriority = 1, int maxPriority = 5, bool onlyUnComplete = false, string orderValue = null)
        {
            IQueryable<Todo> tasks = context.Todos;
            if (startDate != DateTime.MinValue)
            {
                tasks = tasks.Where(t => t.StartDate.Date >= startDate.Date);
            }
            if (endDate != DateTime.MinValue)
            {
                tasks = tasks.Where(t => t.EndDate.Date <= endDate.Date);
            }
            if (minPriority > 1 && minPriority <= maxPriority)
            {
                tasks = tasks.Where(t => t.Priority >= minPriority);
            }
            if (maxPriority <= 5 && maxPriority >= minPriority)
            {
                tasks = tasks.Where(t => t.Priority <= maxPriority);
            }
            if (onlyUnComplete == true)
            {
                tasks = tasks.Where(t => t.IsComplete == false);
            }

            if (orderValue != null)
            {
                switch (orderValue)
                {
                    case "name":
                        tasks = tasks.OrderBy(t => t.Name);
                        break;
                    case "customer":
                        tasks = tasks.OrderBy(t => t.CustomerName);
                        break;
                    case "performer":
                        tasks = tasks.OrderBy(t => t.PerformerName);
                        break;
                    case "startDate":
                        tasks = tasks.OrderBy(t => t.StartDate);
                        break;
                    case "endDate":
                        tasks = tasks.OrderBy(t => t.EndDate);
                        break;
                    case "priority":
                        tasks = tasks.OrderBy(t => t.Priority);
                        break;
                    case "status":
                        tasks = tasks.OrderBy(t => t.IsComplete);
                        break;
                }
            }

            return tasks;
        }

        public void UpdateTask(Todo todo)
        {
            context.Todos.Update(todo);
            context.SaveChanges();
        }

        public void AddEmployeeToTask(Guid todoId, Guid employeeId)
        {
            Todo todo = context.Todos.Find(todoId);
            Employee employee = context.Employees.Find(employeeId);
            todo.Employees.Add(employee);
            context.SaveChanges();
        }

        public void RemoveEmployeeFromTask(Guid todoId, Guid employeeId)
        {
            Todo todo = context.Todos.Include(t => t.Employees).First(t => t.TodoId == todoId);
            Employee employee = context.Employees.Find(employeeId);
            todo.Employees.Remove(employee);
            context.SaveChanges();
        }
        #endregion

        #region Employees
        public Employee GetEmployee(Guid employeeId) => context.Employees.Find(employeeId);

        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }

        public void DeleteEmployee(Guid employeeId)
        {
            context.Employees.Remove(new Employee { EmployeeId = employeeId });
            context.SaveChanges();
        }
        #endregion
    }
}
