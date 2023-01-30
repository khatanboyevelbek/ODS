using ODS.Web.Brokers.Storages;
using ODS.Web.Models;

namespace ODS.Web.Services.Foundations.Employees
{
    public class EmployeeService
    {
        private readonly IStorageBroker storageBroker;

        public EmployeeService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Employee> AddEmployeeAsync(Employee employee) =>
            await this.storageBroker.InsertEmployeeAsync(employee);
    }
}
