using ODS.Web.Models.Foundations;

namespace ODS.Web.Services.Orchestrations.Employees
{
    public interface IEmployeeOrchestrationServices
    {
        IQueryable<Employee> RetrieveEmployeeAscendingOrder(string? orderby);
        ValueTask<int> ImportExternalFileToTable(IFormFile postedFile);
    }
}
