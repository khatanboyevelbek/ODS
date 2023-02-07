using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ODS.Web.Brokers.Loggings;
using ODS.Web.Models;
using ODS.Web.Models.Foundations;
using ODS.Web.Models.Foundations.Exceptions;
using ODS.Web.Services.Processings.Employees;

namespace ODS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IEmployeeProcessingService employeeProcessingService;

        public HomeController(ILoggingBroker loggingBroker,
            IEmployeeProcessingService employeeProcessingService)
        {
            this.loggingBroker = loggingBroker;
            this.employeeProcessingService = employeeProcessingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ValueTask<IActionResult> Index(IFormFile postedFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rows =
                        await this.employeeProcessingService.ImportExternalFileToTable(postedFile);

                    TempData["success"] = string.Format("{0} rows added successfuly", rows);

                    return RedirectToAction("Data");
                }

                TempData["error"] = "File is required";
                return View();
            }
            catch (NotSupportedFileException exception)
            {
                TempData["error"] = $"{exception.Message}";
                return View();
            }
            catch(FormatException exception)
            {
                return RedirectToAction("Error", exception);
            }
        }

        public IActionResult Data(string orderby = null)
        {
            IQueryable<Employee> ascendingOrderedEmployees = null;

            IQueryable<Employee> employees =
                this.employeeProcessingService.RetrieveAllEmployees();

            switch(orderby)
            {
                case "firstname":
                    ascendingOrderedEmployees = employees.OrderBy(x => x.FirstName);
                    break;
                case "lastname":
                    ascendingOrderedEmployees = employees.OrderBy(x => x.LastName);
                    break;
                default:
                    ascendingOrderedEmployees = employees;
                    break;
            }

            return View(ascendingOrderedEmployees);
        }

        public IActionResult Error(Exception exception)
        {
            return View(exception);
        }
    }
}