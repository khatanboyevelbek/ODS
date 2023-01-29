using Microsoft.EntityFrameworkCore;
using ODS.Web.Models;

namespace ODS.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
