using ODS.Web.Models.Foundations;
using ODS.Web.Models.Orchestrations.Files;

namespace ODS.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
        FileConfiguration ConvertSqlDataToXmlFile();
        IQueryable<Employee> RetrieveAllEmployees();
    }
}
