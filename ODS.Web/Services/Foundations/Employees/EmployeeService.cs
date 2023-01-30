using ODS.Web.Brokers.Storages;
using ODS.Web.Models;

namespace ODS.Web.Services.Foundations.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IStorageBroker storageBroker;

        public EmployeeService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Employee> AddEmployeeAsync(Employee employee) =>
            await this.storageBroker.InsertEmployeeAsync(employee);

        public IQueryable<Employee> RetrieveAllEmployees() => 
            this.storageBroker.SelectAllEmployees();

        public async ValueTask<Employee> ModifyEmployeeAsync(Employee employee) => 
            await this.storageBroker.UpdateEmployeeAsync(employee);

        public async ValueTask<Employee> RemoveEmployeeAsync(Employee employee) =>
             await this.storageBroker.DeleteEmployeeAsync(employee);
    }
}
