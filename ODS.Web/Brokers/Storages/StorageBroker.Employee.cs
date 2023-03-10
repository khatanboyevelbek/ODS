using Microsoft.EntityFrameworkCore;
using ODS.Web.Models.Foundations;

namespace ODS.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Employee> Employees { get; set; }

        public async ValueTask<Employee> InsertEmployeeAsync(Employee employee)
        {
            var broker = new StorageBroker(this.Configuration);
            broker.Entry(employee).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return employee;
        }

        public IQueryable<Employee> SelectAllEmployees()
        {
            var broker = new StorageBroker(this.Configuration);
            return broker.Set<Employee>();
        }

        public async ValueTask<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var broker = new StorageBroker(this.Configuration);
            broker.Entry(employee).State = EntityState.Modified;
            await broker.SaveChangesAsync();
            return employee;
        }

        public async ValueTask<Employee> DeleteEmployeeAsync(Employee employee)
        {
            var broker = new StorageBroker(this.Configuration);
            broker.Entry(employee).State = EntityState.Deleted;
            await broker.SaveChangesAsync();
            return employee;
        }
    }
}
