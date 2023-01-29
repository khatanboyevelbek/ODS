using Microsoft.EntityFrameworkCore;
using ODS.Web.Models;

namespace ODS.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .HasIndex(e =>
                    new { e.Id, e.PayrollNumber, e.FirstName,
                        e.LastName, e.DateOfBirth, e.PhoneNumber,
                        e.Address1, e.Address2, e.PostalCode,
                        e.Email, e.StartedDate, e.CreatedDate,
                        e.UpdatedDate});
        }
    }
}
