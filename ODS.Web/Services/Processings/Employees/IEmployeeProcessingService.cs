using ODS.Web.Models.Foundations;

namespace ODS.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
        Task<string> ConvertSqlDataToXmlFile();
        IQueryable<Employee> RetrieveAllEmployees();
    }
}
