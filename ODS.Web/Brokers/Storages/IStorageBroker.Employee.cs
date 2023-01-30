using ODS.Web.Models;

namespace ODS.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Employee> InsertEmployeeAsync(Employee employee);
        IQueryable<Employee> SelectAllEmployees();
        ValueTask<Employee> UpdateEmployeeAsync(Employee employee);
        ValueTask<Employee> DeleteEmployeeAsync(Employee employee);
    }
}
