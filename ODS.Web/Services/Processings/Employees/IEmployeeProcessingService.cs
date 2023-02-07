using ODS.Web.Models.Foundations;

namespace ODS.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        ValueTask<int> ImportExternalFileToTable(IFormFile postedFile);
        IQueryable<Employee> RetrieveAllEmployees();
    }
}
