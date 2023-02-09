using ODS.Web.Brokers.Loggings;
using ODS.Web.Models.Foundations;
using ODS.Web.Services.Processings.Employees;

namespace ODS.Web.Services.Orchestrations.Employees
{
    public class EmployeeOrchestrationServices : IEmployeeOrchestrationServices
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IEmployeeProcessingService employeeProcessingService;

        public EmployeeOrchestrationServices(
            IEmployeeProcessingService employeeProcessingService, 
            ILoggingBroker loggingBroker)
        {
            this.employeeProcessingService = employeeProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<int> ImportExternalFileToTable(IFormFile postedFile) => 
            this.employeeProcessingService.ImportExternalFileToTable(postedFile);

        public IQueryable<Employee> RetrieveEmployeeAscendingOrder(string? orderby)
        {
            IQueryable<Employee> employees =
                this.employeeProcessingService.RetrieveAllEmployees();

            IQueryable<Employee> ascendingOrderedEmployees = orderby switch
            {
                "firstname" => employees.OrderBy(x => x.FirstName),
                "lastname" => employees.OrderBy(x => x.LastName),
                _ => employees
            };

            return ascendingOrderedEmployees;
        }
    }
}
