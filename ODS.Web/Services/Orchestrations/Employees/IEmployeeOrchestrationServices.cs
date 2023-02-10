using ODS.Web.Models.Foundations;
using ODS.Web.Models.Orchestrations.Files;

namespace ODS.Web.Services.Orchestrations.Employees
{
    public interface IEmployeeOrchestrationServices
    {
        IQueryable<Employee> RetrieveEmployeeAscendingOrder(string? orderby);
        Task<int> ImportExternalFileToTable(IFormFile postedFile);
        FileConfiguration ConvertSqlDataToXmlFile();
        Task<FileConfiguration> ConvertSqlDataToJsonFile();
    }
}
