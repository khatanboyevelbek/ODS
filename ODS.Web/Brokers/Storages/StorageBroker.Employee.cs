﻿using Microsoft.EntityFrameworkCore;
using ODS.Web.Models;

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
    }
}
