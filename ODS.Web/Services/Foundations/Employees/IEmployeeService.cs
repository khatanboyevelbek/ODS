﻿using ODS.Web.Models;

namespace ODS.Web.Services.Foundations.Employees
{
    public interface IEmployeeService
    {
        ValueTask<Employee> AddEmployeeAsync(Employee employee);
        IQueryable<Employee> RetrieveAllEmployees();
        ValueTask<Employee> UpdateEmployeeAsync(Employee employee);
    }
}
