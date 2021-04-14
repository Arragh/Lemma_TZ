using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemma_TZ.Models
{
    public interface IDataRepository
    {
        IQueryable<Todo> Todos { get; }
        IQueryable<Employee> Employees { get; }

        Todo GetTask(Guid todoId);
        IEnumerable<Todo> GetFilteredTasks(DateTime startDate, DateTime endDate, int minPriority, int maxPriority, bool onlyUnComplete, string orderValue);
        void AddTask(Todo todo);
        void UpdateTask(Todo todo);
        void DeleteTask(Guid todoId);
        void CompleteTask(Guid todoId);
        void AddEmployeeToTask(Guid todoId, Guid employeeId);
        void RemoveEmployeeFromTask(Guid todoId, Guid employeeId);


        Employee GetEmployee(Guid employeeId);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Guid employeeId);
    }
}
